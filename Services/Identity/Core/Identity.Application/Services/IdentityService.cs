using AutoMapper;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Application.Identity.Dtos;
using BuildingBlocks.Domain.Exceptions;
using Domain;
using Identity.Application.Services.Interfaces;
using Identity.Application.UseCases.Permissions.Queries;
using Identity.Application.UseCases.Roles.Queries;
using Identity.Domain.RoleAggregate;
using Identity.Domain.TenantAggregate;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Services;

public class IdentityService : IIdentityService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantService _tenantService;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public IdentityService(IMediator mediator
        , IMapper mapper
        , ITenantRepository tenantRepository
        , ITenantService tenantService
        , IRoleRepository roleRepository
        , IUserRepository userRepository
        , UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _mapper = mapper;
        _tenantRepository = tenantRepository;
        _tenantService = tenantService;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<IdentityUserDto?> GetByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return _mapper.Map<IdentityUserDto>(user);
    }

    public async Task<IdentityUserDto?> GetByUserNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        return _mapper.Map<IdentityUserDto>(user);
    }

    public async Task<IEnumerable<string>> GetPermissionsForUserAsync(string userId, int tenantId)
    {
        var permissions = await _mediator.Send(new GetAllPermissionForUserQuery(userId, tenantId));
        return permissions.Select(x => x.Name);
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string userId, int tenantId)
    {
        var roles = await _mediator.Send(new GetAllRoleForUserQuery(userId, tenantId));
        return roles;
    }

    public async Task<bool> HasPermissionAsync(string userId, int tenantId, string permission)
    {
        var permissions = await GetPermissionsForUserAsync(userId, tenantId);
        return permissions.Contains(permission);
    }

    public async Task<IdentityResult<IdentityUserDto>> CreateUserAsync(string fullname, string username, string avatarUrl, string email, string password, string? phone, Guid? externalId)
    {
        var user = new ApplicationUser(fullname, avatarUrl, externalId)
        {
            UserName = username,
            Email = email,
            PhoneNumber = phone
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return IdentityResult<IdentityUserDto>.Success(_mapper.Map<IdentityUserDto>(user));
        }

        var error = result.Errors.First();
        return IdentityResult<IdentityUserDto>.Failed(error.Code, error.Description);
    }

    public async Task<IdentityResult<bool>> UpdateAsync(string userId, string fullname, string email, string avatarUrl)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityUserNotFound, $"User with id {userId} not found!");
        }
        
        user.UpdateFullName(fullname);
        user.SetAvatar(avatarUrl);
        user.Email = email;
        
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return IdentityResult<bool>.Success(true);
        }
        
        var error = result.Errors.First();
        return IdentityResult<bool>.Failed(error.Code, error.Description);
    }

    public async Task<IdentityResult<bool>> UpdateUserNameAsync(string userId, string username)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityUserNotFound, $"User with id {userId} not found!");
        }
        
        user.UserName = username;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return IdentityResult<bool>.Success(true);
        }
        
        var error = result.Errors.First();
        return IdentityResult<bool>.Failed(error.Code, error.Description);
    }

    public async Task<IdentityResult<bool>> DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityUserNotFound, $"User with id {userId} not found!");
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return IdentityResult<bool>.Success(true);
        }
        
        var error = result.Errors.First();
        return IdentityResult<bool>.Failed(error.Code, error.Description);
    }

    public async Task<IdentityResult<bool>> GrantToRoleAsync(string userId, string roleName, int tenantId)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityUserNotFound, $"User with id {userId} not found!");
        }

        try
        {
            var role = await _roleRepository.FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityRoleNotFound, $"Role with name {roleName} not found!");
            }

            if (user.Roles.Any(x => x.TenantId == tenantId) && AppRole.All.Contains(roleName))
            {
                return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityDefaultRoleNotAccessible,"Default role can not assign to user");
            }
            
            user.GrantRole(role.Id, tenantId);
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return IdentityResult<bool>.Success(true);
            }
            
            var error = result.Errors.First();
            return IdentityResult<bool>.Failed(error.Code, error.Description);
        }
        catch (CoreException e)
        {
            return IdentityResult<bool>.Failed(e.ErrorCode, e.Message);
        }
    }

    public async Task<IdentityResult<bool>> UnGrantFromRoleAsync(string userId, string roleName, int tenantId)
    {
        var user = await _userRepository.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityUserNotFound, $"User with id {userId} not found!");
        }

        try
        {
            var role = await _roleRepository.FindByNameAsync(roleName);
            if (role == null)
            {
                return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityRoleNotFound, $"Role with name {roleName} not found!");
            }
            
            user.UnGrantRole(role.Id, tenantId);
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return IdentityResult<bool>.Success(true);
            }
            
            var error = result.Errors.First();
            return IdentityResult<bool>.Failed(error.Code, error.Description);
        }
        catch
        {
            return IdentityResult<bool>.Failed(IdentityErrorCodes.IdentityRoleNotFound, $"Role with name {roleName} not found!");
        }
    }
}