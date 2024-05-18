using Application.Dtos.Submissions.Requests;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using BusinessFlow.Application.Clients.Abstracts;
using RestSharp;

namespace BusinessFlow.Application.Clients;

public class SubmissionClient : RestSharpClient, ISubmissionClient
{
    public SubmissionClient(IServiceProvider serviceProvider) : base(serviceProvider, InternalApis.Submission)
    {
    }
    
    public async Task CreateFormAsync(int spaceId, FormRequestDto formDto)
    {
        var request = new RestRequest($"spaces/{spaceId}/forms", Method.Post);
        request.AddJsonBody(formDto);
        
        await ExecuteAsync(request);
    }

    public Task<BusinessFlowBlocksElementsResponse> GetBusinessFlowBlocksElementsAsync(int spaceId, List<Guid> businessFlowBlockIds)
    {
        var request = new RestRequest($"spaces/{spaceId}/business-flows/forms", Method.Get);
        foreach (var id in businessFlowBlockIds)
        {
            request.AddQueryParameter("businessFlowBlockIds", id);
        }

        return ExecuteAsync<BusinessFlowBlocksElementsResponse>(request);
    }
}