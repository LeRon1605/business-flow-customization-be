using Hub.Domain.ConnectionAggregate.Entities;
using Hub.Domain.ConnectionAggregate.Repositories;

namespace Hub.Domain.ConnectionAggregate.DomainService;

public class UserConnectionDomainService : IUserConnectionDomainService
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    
    public UserConnectionDomainService(IUserConnectionRepository userConnectionRepository)
    {
        _userConnectionRepository = userConnectionRepository;
    }
    
    public async Task AddConnectionAsync(string connectionId, string userId, int tenantId)
    {
        var userConnection = await _userConnectionRepository.FindAsync(x => x.UserId == userId 
                                                                      && x.TenantId == tenantId);
        if (userConnection == null)
        {
            userConnection = new UserConnection(userId, tenantId);
            await _userConnectionRepository.InsertAsync(userConnection);   
        }
        
        userConnection.AddConnection(connectionId);
        _userConnectionRepository.Update(userConnection);
    }

    public async Task RemoveConnectionAsync(string connectionId, string userId, int tenantId)
    {
        var connection = await _userConnectionRepository.FindAsync(x => x.UserId == userId 
                                                                        && x.TenantId == tenantId 
                                                                        && x.ConnectionIds.Contains(connectionId));
        if (connection == null)
            return;
        
        connection.RemoveConnection(connectionId);
        
        if (!connection.ConnectionIds.Any())
            _userConnectionRepository.Delete(connection);
    }

    public async Task<List<string>> GetConnectionsAsync(string userId, int tenantId)
    {
        var userConnection = await _userConnectionRepository.FindAsync(x => x.UserId == userId 
                                                                            && x.TenantId == tenantId);
        return userConnection?.ConnectionIds.ToList() ?? new List<string>();
    }

    public async Task<List<string>> GetConnectionsAsync(List<string> userIds, int tenantId)
    {
        var userConnections = await _userConnectionRepository.FindAllAsync(x => userIds.Contains(x.UserId) 
                                                                              && x.TenantId == tenantId);
        return userConnections.SelectMany(x => x.ConnectionIds).ToList();
    }
}