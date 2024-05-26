using System.Net.Http.Json;
using Application.Clients.Interfaces;
using Application.Dtos.Identity;
using BuildingBlocks.Application;
using BuildingBlocks.Domain.Exceptions.Resources;
using Microsoft.AspNetCore.Http;

namespace Application.Clients;

public class IdentityClient : IIdentityClient
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public IdentityClient(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<UserInfoDto> GetUserInfoAsync()
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(InternalApis.Identity),
            DefaultRequestHeaders =
            {
                Authorization = new ("Bearer", _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer", ""))
            }
        };
        
        var response = await httpClient.GetFromJsonAsync<UserInfoDto>("/api/profile");
        if (response == null)
            throw new ResourceUnauthorizedAccessException("User is not authenticated");
        
        return response;
    }
}
