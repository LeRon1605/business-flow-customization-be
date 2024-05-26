using BuildingBlocks.Kernel.Services;

namespace Hub.Application.Services.Abstracts;

public interface IConnectionManager : ISingletonService
{
    void AddConnection(string connectionId, string userId, int tenantId);

    void RemoveConnection(string connectionId, string userId, int tenantId);

    List<string> GetConnections(string userId, int tenantId);
    
    List<string> GetConnections(List<string> userIds, int tenantId);
}