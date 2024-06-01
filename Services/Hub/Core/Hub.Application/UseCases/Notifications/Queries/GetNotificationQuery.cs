using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Hub.Application.Dtos;

namespace Hub.Application.UseCases.Notifications.Queries;

public class GetNotificationQuery : PagingRequestDto, IQuery<PagedResultDto<NotificationDto>>
{
    public bool IsPaging { get; set; }
    
    public GetNotificationQuery(int page, int size, bool isPaging) : base(page, size)
    {
        IsPaging = isPaging;
    }
}