using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Clients;

namespace Hub.Application.Clients.Abstracts;

public interface IInternalBusinessFlowClient : IRestSharpClient
{
    Task<List<BusinessFlowNotificationDataDto>> GetBusinessFlowNotificationDataAsync(int spaceId, List<Guid> businessFlowBlockIds);
    
    Task<List<BusinessFlowNotificationDataDto>> GetExecutionPersonInChargeDataAsync(List<int> submissionIds);
}