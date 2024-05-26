using BuildingBlocks.Domain.Services;
using Domain.Enums;
using Hub.Domain.NotificationAggregate.Entities;

namespace Hub.Domain.NotificationAggregate.DomainServices;

public interface INotificationDomainService : IDomainService
{
    Task<Notification> CreateAsync(string title
        , string content
        , NotificationType type
        , string senderId
        , string receiverId
        , dynamic metaData);
}