using BuildingBlocks.Domain.Models;

namespace Hub.Domain.ConnectionAggregate.Entities;

public class UserConnection : AggregateRoot
{
    public string UserId { get; private set; }
    
    public int TenantId { get; private set; }

    public List<string> ConnectionIds { get; private set; } = new();
    
    public UserConnection(string userId, int tenantId)
    {
        UserId = userId;
        TenantId = tenantId;
    }
    
    public void AddConnection(string connectionId)
    {
        var connection = ConnectionIds.FirstOrDefault(x => x == connectionId);
        if (connection != null)
        {
            return;
        }
        
        ConnectionIds.Add(connectionId);
    }
    
    public void RemoveConnection(string connectionId)
    {
        ConnectionIds.Remove(connectionId);
    }
}