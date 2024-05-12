using Application.Caching.Interfaces;
using Application.Clients.Interfaces;
using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using Application.Identity.Interfaces;

namespace Application.Identity;

public class UserInfoProvider : IUserInfoProvider
{
    private UserInfoDto? _userInfo;
    private readonly IIdentityClient _identityClient;
    private readonly IPermissionCacheManager _permissionCacheManager;
    private readonly IUserCacheManager _userCacheManager;
    
    public UserInfoProvider(IIdentityClient identityClient
        , IPermissionCacheManager permissionCacheManager
        , IUserCacheManager userCacheManager)
    {
        _identityClient = identityClient;
        _permissionCacheManager = permissionCacheManager;
        _userCacheManager = userCacheManager;
    }
    
    public async Task<List<string>> GetRolesAsync(string userId, int tenantId)
    {
        if (_userInfo != null)
            return _userInfo.Roles.ToList();
        
        var roles = await _userCacheManager.GetRolesAsync(userId, tenantId);
        if (roles != null)
            return roles.ToList();
        
        if (_userInfo == null)
        {
            _userInfo = await _identityClient.GetUserInfoAsync();
        }

        return _userInfo.Roles.ToList();
    }

    public async Task<List<string>> GetPermissionsAsync(string userId, int tenantId)
    {
        if (_userInfo != null)
            return _userInfo.Permissions.ToList();
        
        _userInfo = await _identityClient.GetUserInfoAsync();

        return _userInfo.Permissions.ToList();
    }
}