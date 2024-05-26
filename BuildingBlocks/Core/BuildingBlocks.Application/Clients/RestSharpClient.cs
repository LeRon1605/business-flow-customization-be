using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BuildingBlocks.Application.Clients;

public class RestSharpClient 
{
    private readonly string _url;
    private readonly ClientAuthenticationType _authenticationType;
    private readonly ICurrentUser _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected RestClient Client => CreateClient(_url, _authenticationType);
    
    protected RestSharpClient(IHttpContextAccessor httpContextAccessor
        , ICurrentUser currentUser
        , string url
        , ClientAuthenticationType authenticationType = ClientAuthenticationType.JwtForward)
    {
        _url = url;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
        _authenticationType = authenticationType;
    }
    
    private RestClient CreateClient(string url, ClientAuthenticationType authenticationType)
    {
        var options = new RestClientOptions($"{url}/api")
        {
            Authenticator = GetAuthenticator(authenticationType)
        };
        
        return new RestClient(options);
    }

    private IAuthenticator? GetAuthenticator(ClientAuthenticationType authenticationType)
    {
        switch (authenticationType)
        {
            case ClientAuthenticationType.JwtForward:
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(token))
                    return new JwtAuthenticator(token);

                return null;
            
            case ClientAuthenticationType.ClientCredentials:
                var userId = _currentUser.IsAuthenticated ? _currentUser.Id : null;
                var tenantId = _currentUser.IsAuthenticated ? (int?)_currentUser.TenantId : null;
                
                return new ClientCredentialAuthenticator(userId!, tenantId);
            
            default:
                return null;
        }
    }

    protected async Task ExecuteAsync(RestRequest request)
    {
        var response = await Client.ExecuteAsync(request);
        
        ProcessResponse(response);
    }
    
    protected async Task<T> ExecuteAsync<T>(RestRequest request)
    {
        var response = await Client.ExecuteAsync<T>(request);
        
        ProcessResponse(response);

        return response.Data ?? throw new Exception("Internal call error.");;
    }
    
    private void ProcessResponse(RestResponse response)
    {
        if (!response.IsSuccessStatusCode && !string.IsNullOrEmpty(response.Content))
        {
            var message = JsonConvert.DeserializeObject<ErrorResponseDto>(response.Content!);
            if (message is not null)
            {
                throw new ResourceInvalidOperationException(message.Message!, message.Code);   
            }
            else
            {
                throw new Exception("Internal call error.");
            }
        }
    }
}