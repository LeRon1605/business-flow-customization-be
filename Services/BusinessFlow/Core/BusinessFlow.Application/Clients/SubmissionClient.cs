using Application.Dtos.Submissions.Requests;
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
    
    public async Task CreateFormAsync(int spaceId, CreateFormRequestDto createFormDto)
    {
        var request = new RestRequest($"spaces/{spaceId}/forms", Method.Post);
        request.AddJsonBody(createFormDto);
        
        await PostAsync(request);
    }
}