using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BuildingBlocks.Application.Clients;

public class RestSharpClient 
{
    protected readonly RestClient Client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    protected RestSharpClient(IServiceProvider serviceProvider
        , string url
        , ClientAuthenticationType authenticationType = ClientAuthenticationType.JwtForward)
    {
        _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

        var options = new RestClientOptions($"{url}/api")
        {
            Authenticator = GetAuthenticator(authenticationType)
        };
        Client = new RestClient(options);
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
                throw new NotImplementedException();
            
            default:
                return null;
        }
    }

    protected async Task PostAsync(RestRequest request)
    {
        var response = await Client.ExecuteAsync(request);
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