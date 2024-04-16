using Application.Identity.Interfaces;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Presentation.Authorization;
using Microsoft.AspNetCore.Http;

namespace Presentation.Authorization;

public class MicroserviceCurrentUser : CurrentUser, ICurrentUser
{
    private readonly IUserInfoProvider _userInfoProvider;

    public MicroserviceCurrentUser(IHttpContextAccessor httpContextAccessor, IUserInfoProvider userInfoProvider) : base(httpContextAccessor)
    {
        _userInfoProvider = userInfoProvider;
    }

    public override async Task<List<string>> GetRolesAsync()
    {
        if (IsAuthenticated)
        {
            return await _userInfoProvider.GetRolesAsync(Id, TenantId);
        }

        throw new ResourceUnauthorizedAccessException("User is not authenticated");
    }
    
    public override async Task<List<string>> GetPermissionsAsync()
    {
        if (IsAuthenticated)
        {
            return await _userInfoProvider.GetPermissionsAsync(Id, TenantId);
        }

        throw new ResourceUnauthorizedAccessException("User is not authenticated");
    }
}