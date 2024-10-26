﻿using Application.Caching.Interfaces;
using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application.Caching;
using BuildingBlocks.Application.Caching.Interfaces;

namespace Application.Caching;

public class PermissionCacheManager : CachingManager<PermissionCacheManager>, IPermissionCacheManager
{
    private readonly ICachingService _cacheService;
    
    public PermissionCacheManager(ICachingService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public Task<IEnumerable<PermissionDto>?> GetForRoleAsync(string role)
    {
        var key = KeyGenerator.Generate("PermissionRole", role);
        return _cacheService.GetRecordAsync<IEnumerable<PermissionDto>?>(key);
    }

    public Task SetForRoleAsync(string role, IEnumerable<PermissionDto> permissions)
    {
        var key = KeyGenerator.Generate("PermissionRole", role);
        return _cacheService.SetRecordAsync<IEnumerable<PermissionDto>?>(key, permissions, TimeSpan.FromMinutes(30));
    }

    public Task RemoveForRoleAsync(string role)
    {
        var key = KeyGenerator.Generate("PermissionRole", role);
        return _cacheService.RemoveRecordAsync(key);
    }
}