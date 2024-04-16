using BuildingBlocks.Domain.Models;

namespace Identity.Domain.TenantAggregate.Entities;

public class Tenant : AggregateRoot
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    
    public Tenant(string name, string avatarUrl)
    {
        Name = Guard.NotNullOrEmpty(name, nameof(Name));
        AvatarUrl = Guard.NotNullOrEmpty(avatarUrl, nameof(AvatarUrl));
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