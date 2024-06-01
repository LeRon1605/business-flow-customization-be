using BuildingBlocks.Application.Dtos;

namespace Hub.Application.UseCases.Notifications.Dtos;

public class NotificationPagingRequestDto : PagingRequestDto
{
    public bool IsPaging { get; set; }
}