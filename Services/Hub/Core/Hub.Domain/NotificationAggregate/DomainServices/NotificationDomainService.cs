using Domain.Enums;
using Hub.Domain.NotificationAggregate.Entities;
using Hub.Domain.NotificationAggregate.Repositories;

namespace Hub.Domain.NotificationAggregate.DomainServices;

public class NotificationDomainService : INotificationDomainService
{
    private readonly INotificationRepository _notificationRepository;
    
    public NotificationDomainService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    
    public async Task<Notification> CreateAsync(string title
        , string content
        , NotificationType type
        , string senderId
        , string receiverId
        , dynamic metaData)
    {
        var notification = new Notification(title, content, type, senderId, receiverId, metaData);
        
        await _notificationRepository.InsertAsync(notification);
        
        return notification;
    }
}