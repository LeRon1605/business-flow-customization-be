using BuildingBlocks.Domain.Models;
using Identity.Domain.TenantAggregate.Enums;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Domain.TenantAggregate.Entities;

public class Tenant : AuditableAggregateRoot
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public List<UserInTenant> Users { get; set; }
    public List<TenantInvitation> Invitations { get; set; }
    
    public Tenant(string name, string avatarUrl)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        AvatarUrl = Guard.NotNullOrEmpty(avatarUrl, nameof(AvatarUrl));
        Users = new List<UserInTenant>();
        Invitations = new List<TenantInvitation>();
    }
    
    public void Update(string name, string avatarUrl)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        AvatarUrl = Guard.NotNullOrEmpty(avatarUrl, nameof(AvatarUrl));
    }
    
    public void InviteUser(string email, string roleId)
    {
        var isInvited = Invitations.Any(x => x.Email == email && x.Status == TenantInvitationStatus.Pending);
        if (isInvited)
        {
            throw new TenantInvitationAlreadyExistedException(email);
        }
        
        var isUserExisted = Users.Any(x => x.User.Email == email);
        if (isUserExisted)
        {
            throw new UserAlreadyInTenantException(email);
        }
        
        Invitations.Add(new TenantInvitation(email, Id, roleId));
    }
    
    public void AcceptInvitation(string token)
    {
        var invitation = Invitations.FirstOrDefault(x => x.Token == token 
                                                         && x.Status == TenantInvitationStatus.Pending);
        if (invitation == null)
        {
            throw new TenantInvitationNotFoundException(token);
        }
        
        invitation.Accept();
    }
    
    private Tenant()
    {
        
    }
}