using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Hub.Application.Dtos;

namespace Hub.Application.UseCases.Notifications.Queries;

public class GetNotificationQuery : PagingRequestDto, IQuery<PagedResultDto<NotificationDto>>
{
    public GetNotificationQuery(int page, int size) : base(page, size)
    {
    }
}