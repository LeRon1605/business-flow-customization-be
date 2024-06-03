using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using BuildingBlocks.Application.Identity;
using Hub.Application.Clients.Abstracts;
using Microsoft.AspNetCore.Http;
using RestSharp;

namespace Hub.Application.Clients;

public class InternalSubmissionClient : RestSharpClient, IInternalSubmissionClient
{
    public InternalSubmissionClient(IServiceProvider serviceProvider) 
        : base(serviceProvider, InternalApis.Submission, ClientAuthenticationType.ClientCredentials)
    {
    }

    public async Task<List<SubmissionNotificationDataDto>> GetSubmissionNotificationDataAsync(List<int> submissionIds)
    {
        var request = new RestRequest("notifications/submissions/data", Method.Post);
        
        request.AddJsonBody(new GetSubmissionNotificationDataRequestDto
        {
            SubmissionIds = submissionIds
        });

        return await ExecuteAsync<List<SubmissionNotificationDataDto>>(request);
    }
}