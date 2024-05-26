using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.Notifications.Queries;

public class GetBusinessFlowNotificationDataQuery : IQuery<List<BusinessFlowNotificationDataDto>>
{
    public int SpaceId { get; set; }

    public List<GetBusinessFlowNotificationDataEntityRequestDto> Entities { get; set; } = null!;
    
    public GetBusinessFlowNotificationDataQuery(int spaceId, List<GetBusinessFlowNotificationDataEntityRequestDto> entities)
    {
        SpaceId = spaceId;
        Entities = entities;
    }
}