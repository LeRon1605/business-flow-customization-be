using BuildingBlocks.Domain.Services;

namespace Hub.Domain.ConnectionAggregate.DomainService;

public interface IUserConnectionDomainService : IDomainService
{
    Task AddConnectionAsync(string connectionId, string userId, int tenantId);

    Task RemoveConnectionAsync(string connectionId, string userId, int tenantId);

    Task<List<string>> GetConnectionsAsync(string userId, int tenantId);
    
    Task<List<string>> GetConnectionsAsync(List<string> userIds, int tenantId);
}