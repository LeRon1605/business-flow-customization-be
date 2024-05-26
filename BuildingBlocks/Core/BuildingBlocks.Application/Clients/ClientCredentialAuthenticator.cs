using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Shared.Helpers;
using Domain.Claims;
using IdentityModel.Client;
using RestSharp;
using RestSharp.Authenticators;

namespace BuildingBlocks.Application.Clients;

public class ClientCredentialAuthenticator : IAuthenticator
{
    private readonly string? _userId;
    private readonly int? _tenantId;
    
    public ClientCredentialAuthenticator(string? userId, int? tenantId)
    {
        _userId = userId;
        _tenantId = tenantId;
    }
    
    public async ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = InternalApis.Identity + "/connect/token",
            ClientId = AssemblyHelper.GetServiceName(),
            ClientSecret = AssemblyHelper.GetServiceName(),
            Scope = "microservice"
        });
        
        if (response.IsError)
            throw new ResourceUnauthorizedAccessException(response.Error);

        if (!string.IsNullOrEmpty(_userId) && _tenantId.HasValue)
        {
            request.AddHeader(AppClaim.MicroserviceUserId, _userId);
            request.AddHeader(AppClaim.MicroserviceTenantId, _tenantId.Value.ToString());
        }
        
        request.AddHeader("Authorization", "Bearer " + response.AccessToken);
    }
}