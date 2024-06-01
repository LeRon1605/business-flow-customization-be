using ApiGateway.Clients.Abstracts;
using Application.Dtos.Spaces;
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
}