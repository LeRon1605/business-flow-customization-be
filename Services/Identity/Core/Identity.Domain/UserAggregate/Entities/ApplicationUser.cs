using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Models.Interfaces;
using Domain;
using Domain.Identities;
using Domain.Roles;
using Identity.Domain.TenantAggregate.DomainEvents;
using Identity.Domain.UserAggregate.DomainEvents;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.UserAggregate.Entities;

public partial class ApplicationUser : IdentityUser<string>
{
    public string FullName { get; private set; }
    public string AvatarUrl { get; private set; }
    public Guid? ExternalId { get; private set; }
    public List<RefreshToken> RefreshToken { get; set; }
    public List<UserInTenant> Tenants { get; set; }
    public List<ApplicationUserInRole> Roles { get; set; }

    public ApplicationUser(string fullname, string? avatarUrl = null, Guid? externalId = null)
    {
        Id = Guid.NewGuid().ToString();
        FullName = Guard.NotNullOrEmpty(fullname, nameof(FullName));
        AvatarUrl = string.IsNullOrWhiteSpace(avatarUrl) ? "https://static.thenounproject.com/png/5034901-200.png" : avatarUrl;
        ExternalId = externalId;
        RefreshToken = new List<RefreshToken>();
        Tenants = new List<UserInTenant>();
        Roles = new List<ApplicationUserInRole>();
    }

    public static ApplicationUser Default()
    {
        return new ApplicationUser()
        {
            Id = AppUser.Id,
            UserName = "admin@gmail.com",
            Email = "email@gmail.com",
            FullName = "Business Flow",
            AvatarUrl = AppUser.DefaultAvatar,
        };
    }

    public void UpdateFullName(string fullName)
    {
        FullName = Guard.NotNullOrEmpty(fullName, nameof(FullName));
    }

    public void SetAvatar(string avatar)
    {
        AvatarUrl = Guard.NotNullOrWhiteSpace(avatar, nameof(AvatarUrl));
    }

    public void AddRefreshToken(string accessTokenId, string refreshToken, DateTime expireAt)
    {
        if (GetRefreshToken(accessTokenId, refreshToken) != null)
        {
            throw new RefreshTokeHasAlreadyAddedException(refreshToken);
        }
        
        RefreshToken.Add(new RefreshToken(accessTokenId, refreshToken, expireAt, Id));
    }

    public void UseRefreshToken(string accessTokenId, string refreshToken)
    {
        if (CanRefreshToken(accessTokenId, refreshToken))
        {
            RefreshToken.Remove(GetRefreshToken(accessTokenId, refreshToken));
        }
    }

    public void ClearRole(int tenantId)
    {
        var deletedRole = Roles
            .Where(x => !AppRole.All.Contains(x.Role!.Name!) 
                        && x.TenantId == tenantId)
            .ToList();
        foreach (var role in deletedRole)
        {
            Roles.Remove(role);
        }
        
        AddDomainEvent(new UserRoleUpdatedDomainEvent(Id, tenantId));
    }

    public void GrantRole(string roleId, int tenantId)
    {
        if (Roles.Any(x => x.RoleId == roleId && x.TenantId == tenantId))
        {
            throw new ResourceAlreadyExistException("Role has already granted to user in this tenant!");
        }
        
        Roles.Add(new ApplicationUserInRole(Id, roleId, tenantId));
        AddDomainEvent(new UserRoleUpdatedDomainEvent(Id, tenantId));
    }
    
    public void UnGrantRole(string roleId, int tenantId)
    {
        var role = Roles.FirstOrDefault(x => x.RoleId == roleId && x.TenantId == tenantId);

        if (role == null)
        {
            throw new ResourceNotFoundException("Role not found!");
        }

        Roles.Remove(role);
        AddDomainEvent(new UserRoleUpdatedDomainEvent(Id, tenantId));
    }

    public void AddToTenant(int tenantId, bool isOwner)
    {
        if (Tenants.Any(x => x.TenantId == tenantId))
        {
            throw new UserAlreadyInTenantException(Id, tenantId);
        }
        
        Tenants.Add(new UserInTenant(tenantId, Id, isOwner));
    }
    
    public void UpdateProfileInTenant(string fullName, string avatarUrl, int tenantId)
    {
        var tenant = Tenants.FirstOrDefault(x => x.TenantId == tenantId);

        if (tenant == null)
        {
            throw new UserNotInTenantException(Id, tenantId);
        }

        // tenant.Update(fullName, avatarUrl);
    }

    public void RemoveFromTenant(int tenantId)
    {
        var tenant = Tenants.FirstOrDefault(x => x.TenantId == tenantId);
        var role = Roles.FirstOrDefault(x => x.TenantId == tenantId);

        if (tenant == null || role == null)
        {
            throw new UserNotInTenantException(Id, tenantId);
        }

        Tenants.Remove(tenant);
        Roles.Remove(role);
        AddDomainEvent(new RemoveUserInTenantDomainEvent(Id));
    }

    public string GetDefaultRole(int tenantId)
    {
        return Roles.Where(x => x.TenantId == tenantId)
            .Select(x => x.Role)
            .Where(x => x!.IsDefaultRole())
            .Select(x => x!.Name)
            .FirstOrDefault()!;
    }

    private bool CanRefreshToken(string accessTokenId, string refreshToken)
    {
        var token = GetRefreshToken(accessTokenId, refreshToken);
        
        if (token == null)
        {
            throw new RefreshTokenNotFoundException(refreshToken);
        }

        if (token.IsExpire())
        {
            throw new RefreshTokenHasAlreadyExpireException(refreshToken);
        }

        return true;
    }

    private RefreshToken? GetRefreshToken(string accessTokenId, string refreshToken)
    {
        return RefreshToken.FirstOrDefault(x => x.AccessTokenId == accessTokenId && x.Value == refreshToken);
    }

    private ApplicationUser()
    {
        RefreshToken = new List<RefreshToken>();
    }
}