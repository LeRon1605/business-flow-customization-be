using Application.Clients.Interfaces;
using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application;
using BuildingBlocks.Application.Clients;
using RestSharp;

namespace Application.Clients;

public class IdentityClient : RestSharpClient, IIdentityClient
{
    public IdentityClient(IServiceProvider serviceProvider) : base(serviceProvider, InternalApis.Identity)
    {
    }


    public async Task<UserInfoDto> GetUserInfoAsync()
    {
        var request = new RestRequest("profile");
        return await Client.GetAsync<UserInfoDto>(request);
    }
}
