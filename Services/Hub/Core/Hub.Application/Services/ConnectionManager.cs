using Hub.Application.Services.Abstracts;
using Hub.Domain.ConnectionAggregate.Entities;

namespace Hub.Application.Services;

public class ConnectionManager : IConnectionManager
{
    private readonly List<UserConnection> _connections = new();
    
    public void AddConnection(string connectionId, string userId, int tenantId)
    {
        var connection = _connections.FirstOrDefault(x => x.UserId == userId 
                                                          && x.TenantId == tenantId);
        if (connection == null)
            connection = new UserConnection(userId, tenantId);

        connection.AddConnection(connectionId);
        
        _connections.Add(connection);
    }
    
    public void RemoveConnection(string connectionId, string userId, int tenantId)
    {
        var connection = _connections.FirstOrDefault(x => x.UserId == userId 
                                                          && x.TenantId == tenantId 
                                                          && x.ConnectionIds.Contains(connectionId));
        if (connection == null)
            return;
        
        connection.RemoveConnection(connectionId);
        
        if (!connection.ConnectionIds.Any())
            _connections.Remove(connection);
    }

    public List<string> GetConnections(string userId, int tenantId)
    {
        return _connections.Where(x => x.UserId == userId
                                       && x.TenantId == tenantId)
            .SelectMany(x => x.ConnectionIds)
            .ToList();
    }

    public List<string> GetConnections(List<string> userIds, int tenantId)
    {
        return _connections
            .Where(x => userIds.Contains(x.UserId)
                        && x.TenantId == tenantId)
            .SelectMany(x => x.ConnectionIds)
            .ToList();
    }
}