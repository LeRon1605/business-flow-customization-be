using BuildingBlocks.Domain.Models;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Domain.TenantAggregate.Entities;

public class Tenant : AggregateRoot
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public List<UserInTenant> Users { get; set; }
    
    public Tenant(string name, string avatarUrl)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        AvatarUrl = Guard.NotNullOrEmpty(avatarUrl, nameof(AvatarUrl));
        Users = new List<UserInTenant>();
    }
    
    public void Update(string name, string avatarUrl)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        AvatarUrl = Guard.NotNullOrEmpty(avatarUrl, nameof(AvatarUrl));
    }
    
    private Tenant()
    {
        
    }
}