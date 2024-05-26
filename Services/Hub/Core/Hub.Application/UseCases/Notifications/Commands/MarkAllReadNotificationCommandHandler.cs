using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Hub.Domain.NotificationAggregate.Repositories;

namespace Hub.Application.UseCases.Notifications.Commands;

public class MarkAllReadNotificationCommandHandler : ICommandHandler<MarkAllReadNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUser _currentUser;
    
    public MarkAllReadNotificationCommandHandler(INotificationRepository notificationRepository
        , ICurrentUser currentUser)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
    }
    
    public Task Handle(MarkAllReadNotificationCommand request, CancellationToken cancellationToken)
    {
        return _notificationRepository.MarkAllReadAsync(_currentUser.Id);
    }
}