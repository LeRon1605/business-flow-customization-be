using ApiGateway.Clients.Abstracts;
using Application.Dtos.Spaces;
using Application.Dtos.SubmissionExecutions;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using RestSharp;

namespace ApiGateway.Clients;

public class BusinessFlowClient : RestSharpClient, IBusinessFlowClient
{
    public BusinessFlowClient(IServiceProvider serviceProvider)
        : base(serviceProvider, InternalApis.BusinessFlow)
    {
    }

    public async Task<List<SpaceDto>> GetSpacesAsync()
    {
        var request = new RestRequest("spaces");
        return await ExecuteAsync<List<SpaceDto>>(request);
    }
    
    public async Task<List<AssignedSubmissionExecutionDto>> GetAssignedSubmissionExecutionsAsync()
    {
        var request = new RestRequest("business-flows/in-charge-executions");
        return await ExecuteAsync<List<AssignedSubmissionExecutionDto>>(request);
    }
}