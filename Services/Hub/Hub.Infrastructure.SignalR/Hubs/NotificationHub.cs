using BuildingBlocks.Application.Identity;
using Hub.Application.Services.Abstracts;

namespace Hub.Infrastructure.SignalR.Hubs;

public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly ICurrentUser _currentUser;
    private readonly IConnectionManager _connectionManager;
    
    public NotificationHub(ICurrentUser currentUser
        , IConnectionManager connectionManager)
    {
        _currentUser = currentUser;
        _connectionManager = connectionManager;
    }
    
    public override async Task OnConnectedAsync()
    {
        _connectionManager.AddConnection(Context.ConnectionId
            , _currentUser.Id
            , _currentUser.TenantId);
        
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _connectionManager.RemoveConnection(Context.ConnectionId
            , _currentUser.Id
            , _currentUser.TenantId);
        
        await base.OnDisconnectedAsync(exception);
    }
}