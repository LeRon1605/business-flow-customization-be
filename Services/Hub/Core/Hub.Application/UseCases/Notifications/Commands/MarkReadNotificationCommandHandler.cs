using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Hub.Domain.NotificationAggregate.Repositories;

namespace Hub.Application.UseCases.Notifications.Commands;

public class MarkReadNotificationCommandHandler : ICommandHandler<MarkReadNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUser _currentUser;
    
    public MarkReadNotificationCommandHandler(INotificationRepository notificationRepository
        , ICurrentUser currentUser)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
    }
    
    public Task Handle(MarkReadNotificationCommand request, CancellationToken cancellationToken)
    {
        return _notificationRepository.MarkReadAsync(request.Id, _currentUser.Id);
    }
}