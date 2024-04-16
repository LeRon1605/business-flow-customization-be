using Application.Caching.Interfaces;
using Application.Dtos;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Identity.Domain.PermissionAggregate.Entities;
using Identity.Domain.PermissionAggregate.Specifications;
using Identity.Domain.RoleAggregate;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionForUserQueryHandler : IQueryHandler<GetAllPermissionForUserQuery, IEnumerable<PermissionDto>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBasicReadOnlyRepository<ApplicationPermission, int> _permissionRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserCacheManager _userCacheManager;
    private readonly IPermissionCacheManager _permissionCacheManager;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPermissionQueryHandler> _logger;
    
    public GetAllPermissionForUserQueryHandler(IRoleRepository roleRepository
        , IUserRepository userRepository
        , IUserCacheManager userCacheManager
        , IPermissionCacheManager permissionCacheManager
        , IBasicReadOnlyRepository<ApplicationPermission, int> permissionRepository
        , UserManager<ApplicationUser> userManager
        , IMapper mapper
        , ILogger<GetAllPermissionQueryHandler> logger)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _userManager = userManager;
        _userCacheManager = userCacheManager;
        _permissionCacheManager = permissionCacheManager;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionForUserQuery request, CancellationToken cancellationToken)
    {
        var roles = await GetRolesForUserAsync(request.UserId, request.TenantId);
        
        var result = new List<PermissionDto>();
        foreach (var role in roles)
        {
            var permissions = await GetPermissionsForRoleAsync(role);
            result.AddRange(permissions);
        }

        return result;
    }
    
    private async Task<IEnumerable<string>> GetRolesForUserAsync(string userId, int tenantId)
    {
        var roles = await _userCacheManager.GetRolesAsync(userId, tenantId);
        if (roles == null)
        {
            _logger.LogInformation("[Get role] Cache missed!");
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            roles = await _userRepository.GetRolesAsync(userId, tenantId);
            await _userCacheManager.SetRolesAsync(userId, tenantId, roles);
        }

        return roles;
    }

    private async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string name)
    {
        var permissions = await _permissionCacheManager.GetForRoleAsync(name);
        if (permissions == null)
        {
            _logger.LogInformation("[Get permissions] Cache missed!");
            
            var role = await _roleRepository.FindByNameAsync(name);
            if (role == null)
            {
                throw new RoleNotFoundException(nameof(ApplicationRole.Name), name);
            }

            var specification = new PermissionInSpecification(role.Permissions.Select(x => x.PermissionId));
            permissions = _mapper.Map<List<PermissionDto>>(await _permissionRepository.FilterAsync(specification));
            
            await _permissionCacheManager.SetForRoleAsync(role.Name!, permissions);
        }

        return permissions;
    }
}