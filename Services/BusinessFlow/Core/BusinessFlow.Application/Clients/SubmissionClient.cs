using Application.Dtos.Submissions.Requests;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BusinessFlow.Application.Clients.Abstracts;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace BusinessFlow.Application.Clients;

public class SubmissionClient : RestSharpClient, ISubmissionClient
{
    public SubmissionClient(IServiceProvider serviceProvider) : base(serviceProvider, InternalApis.Submission)
    {
    }
    
    public async Task<SimpleIdResponse<int>> CreateFormAsync(int spaceId, FormRequestDto formDto)
    {
        var request = new RestRequest($"spaces/{spaceId}/forms", Method.Post);
        request.AddJsonBody(formDto);
        
        return await ExecuteAsync<SimpleIdResponse<int>>(request);
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