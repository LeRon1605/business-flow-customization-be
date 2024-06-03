using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Notifications.Queries;

public class GetUserNotificationDataQuery : IQuery<IList<IdentityNotificationDataDto>>
{
    public List<string> UserIds { get; set; }
    
    public GetUserNotificationDataQuery(List<string> userIds)
    {
        UserIds = userIds;
    }
}