using Domain;
using Domain.Roles;
using Identity.Domain.RoleAggregate.DomainEvents;
using Identity.Domain.RoleAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.RoleAggregate.Entities;

public partial class ApplicationRole : IdentityRole
{
    public List<RolePermission> Permissions { get; set; }
        
    public ApplicationRole(string roleName) : base(roleName)
    {
        if (IsDefaultRole(roleName))
        {
            throw new DefaultRoleAccessDeniedException(Name);    
        }
        
        Permissions = new List<RolePermission>();
    }
    
    public static List<ApplicationRole> GetDefaultRoles()
    {
        return AppRole.All.Select(x => new ApplicationRole()
        {
            Name = x
        }).ToList();
    }

    private ApplicationRole()
    {
        
    }

    public void ClearPermission()
    {
        if (IsDefaultRole(Name))
        {
            throw new DefaultRoleAccessDeniedException(Name);    
        }
        
        Permissions.Clear();
        AddDomainEvent(new PermissionForRoleUpdatedDomainEvent(Name));
    }
    
    public void GrantPermission(int permissionId)
    {
        if (HasPermission(permissionId))
        {
            throw new PermissionHasAlreadyGrantedToRoleException(Id, permissionId);
        }
        
        Permissions.Add(new RolePermission(Id, permissionId));
        AddDomainEvent(new PermissionForRoleUpdatedDomainEvent(Name));
    }
    
    public void UnGrantPermission(int permissionId)
    {
        var permission = Permissions.FirstOrDefault(x => x.PermissionId == permissionId);
        if (permission == null)
        {
            throw new PermissionHasNotGrantedToRoleException(Id, permissionId);
        }
        
        Permissions.Remove(permission);
        AddDomainEvent(new PermissionForRoleUpdatedDomainEvent(Name));
    }

    public void Update(string name)
    {
        if (IsDefaultRole(Name) || IsDefaultRole(name))
        {
            throw new DefaultRoleAccessDeniedException(name);    
        }
        
        AddDomainEvent(new RoleNameUpdatedDomainEvent(Name));
        Name = name;
    }

    public void Destroy()
    {
        if (IsDefaultRole(Name))
        {
            throw new DefaultRoleAccessDeniedException(Name);    
        }
    }

    private bool HasPermission(int permissionId)
    {
        return Permissions.Any(x => x.PermissionId == permissionId);
    }

    public bool IsDefaultRole(string name)
    {
        return AppRole.All.Contains(name);
    }
    
    public bool IsDefaultRole()
    {
        return AppRole.All.Contains(Name);
    }
}