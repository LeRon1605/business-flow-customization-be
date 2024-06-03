using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using Hub.Application.Clients.Abstracts;
using RestSharp;

namespace Hub.Application.Clients;

public class InternalBusinessFlowClient : RestSharpClient, IInternalBusinessFlowClient
{
    public InternalBusinessFlowClient(IServiceProvider serviceProvider) 
        : base(serviceProvider, InternalApis.BusinessFlow, ClientAuthenticationType.ClientCredentials)
    {
    }

    public async Task<List<BusinessFlowNotificationDataDto>> GetBusinessFlowNotificationDataAsync(int spaceId, List<Guid> businessFlowBlockIds)
    {
        var request = new RestRequest("notifications/business-flows/data", Method.Post);
        
        request.AddJsonBody(new GetBusinessFlowNotificationDataRequestDto
        {
            SpaceId = spaceId,
            Entities = businessFlowBlockIds
                .Select(x => new GetBusinessFlowNotificationDataEntityRequestDto { Id = x.ToString(), Type = BusinessFlowNotificationDataType.BusinessFlow})
                .ToList()
        });

        return await ExecuteAsync<List<BusinessFlowNotificationDataDto>>(request);
    }

    public Task<List<BusinessFlowNotificationDataDto>> GetExecutionPersonInChargeDataAsync(List<int> submissionIds)
    {
        var request = new RestRequest("notifications/business-flows/data", Method.Post);
        
        request.AddJsonBody(new GetBusinessFlowNotificationDataRequestDto
        {
            Entities = submissionIds
                .Select(x => new GetBusinessFlowNotificationDataEntityRequestDto
                {
                    Id = x.ToString(), 
                    Type = BusinessFlowNotificationDataType.ExecutionPersonInCharge
                })
                .ToList()
        });

        return ExecuteAsync<List<BusinessFlowNotificationDataDto>>(request);
    }
}