using ApiGateway.Clients.Abstracts;
using Application.Dtos.Forms;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using RestSharp;

namespace ApiGateway.Clients;

public class SubmissionClient : RestSharpClient, ISubmissionClient
{
    public SubmissionClient(IServiceProvider serviceProvider) 
        : base(serviceProvider, InternalApis.Submission)
    {
    }

    public Task<List<BasicFormDto>> GetFormsBySpacesAsync(List<int> spaceIds)
    {
        var request = new RestRequest("forms/spaces");
        foreach (var spaceId in spaceIds)
        {
            request.AddQueryParameter("spaceIds", spaceId);
        }
        
        return ExecuteAsync<List<BasicFormDto>>(request);
    }
}