using ApiGateway.Clients.Abstracts;
using Application.Dtos.Forms;
using Application.Dtos.Submissions.Responses;
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
    
    public Task<List<BasicSubmissionDto>> GetSubmissionDataAsync(List<int> submissionIds)
    {
        var request = new RestRequest("submissions/data");
        foreach (var submissionId in submissionIds)
        {
            request.AddQueryParameter("submissionIds", submissionId);
        }
        
        return ExecuteAsync<List<BasicSubmissionDto>>(request);
    }
}