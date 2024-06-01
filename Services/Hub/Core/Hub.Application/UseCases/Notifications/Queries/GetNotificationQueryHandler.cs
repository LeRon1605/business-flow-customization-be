using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using Hub.Application.Dtos;
using Hub.Domain.NotificationAggregate.Repositories;

namespace Hub.Application.UseCases.Notifications.Queries;

public class GetNotificationQueryHandler : IQueryHandler<GetNotificationQuery, PagedResultDto<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;
    
    public GetNotificationQueryHandler(INotificationRepository notificationRepository
        , ICurrentUser currentUser
        , IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
        _mapper = mapper;
    }
    
    public async Task<PagedResultDto<NotificationDto>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var notifications = request.IsPaging
            ? await _notificationRepository.GetPagedAsync(request.Page, request.Size, _currentUser.Id)
            : await _notificationRepository.GetAsync(_currentUser.Id);
        var total = await _notificationRepository.GetCountAsync(_currentUser.Id);

        return new PagedResultDto<NotificationDto>(total, request.Size, _mapper.Map<List<NotificationDto>>(notifications));
    }
}