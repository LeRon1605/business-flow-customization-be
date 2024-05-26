using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Hub.Domain.NotificationAggregate.Repositories;

namespace Hub.Application.UseCases.Notifications.Queries;

public class GetUnReadNotificationCountQueryHandler : IQueryHandler<GetUnReadNotificationCountQuery, int>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUser _currentUser;
    
    public GetUnReadNotificationCountQueryHandler(INotificationRepository notificationRepository
        , ICurrentUser currentUser)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
    }
    
    public async Task<int> Handle(GetUnReadNotificationCountQuery request, CancellationToken cancellationToken)
    {
        return await _notificationRepository.GetUnReadCountAsync(_currentUser.Id);
    }
}