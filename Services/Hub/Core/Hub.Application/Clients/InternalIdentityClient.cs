using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using Hub.Application.Clients.Abstracts;
using RestSharp;

namespace Hub.Application.Clients;

public class InternalIdentityClient : RestSharpClient, IInternalIdentityClient
{
    public InternalIdentityClient(IServiceProvider serviceProvider) 
        : base(serviceProvider, InternalApis.Identity, ClientAuthenticationType.ClientCredentials)
    {
    }
    
    public Task<List<IdentityNotificationDataDto>> GetIdentityNotificationDataAsync(List<string> userIds)
    {
        var request = new RestRequest("api/notifications/users/data");

        foreach (var userId in userIds)
        {
            request.AddQueryParameter("userIds", userId);
        }
        
        return ExecuteAsync<List<IdentityNotificationDataDto>>(request);
    }
}