using BuildingBlocks.Kernel.Services;
using Domain.Enums;

namespace Hub.Application.Services.Abstracts;

public interface INotificationSenderService : IScopedService
{
    Task SendAsync(string receiverId
        , int tenantId
        , string data
        , NotificationType type);
    
    Task SendAsync(List<string> userIds
        , int tenantId
        , string data
        , NotificationType type);
    
    Task SendRealTimeAsync(string receiverId
        , int tenantId
        , string data
        , NotificationType type);
    
    Task SendRealTimeAsync(List<string> userIds
        , int tenantId
        , string data
        , NotificationType type);
}