using Application.Caching.Interfaces;
using BuildingBlocks.Application.Cqrs;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetAllRoleForUserQueryHandler : IQueryHandler<GetAllRoleForUserQuery, IEnumerable<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserCacheManager _userCacheManager;
    private readonly ILogger<GetAllRoleForUserQueryHandler> _logger;
    
    public GetAllRoleForUserQueryHandler(IUserRepository userRepository
        , IUserCacheManager userCacheManager
        , UserManager<ApplicationUser> userManager
        , ILogger<GetAllRoleForUserQueryHandler> logger)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _userCacheManager = userCacheManager;
        _logger = logger;
    }
    
    public async Task<IEnumerable<string>> Handle(GetAllRoleForUserQuery request, CancellationToken cancellationToken)
    {
        var roles = await _userCacheManager.GetRolesAsync(request.UserId, request.TenantId);
        if (roles == null)
        {
            _logger.LogInformation("[Get role] Cache missed!");
            
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }
            
            roles = await _userRepository.GetRolesAsync(request.UserId, request.TenantId);
            await _userCacheManager.SetRolesAsync(request.UserId, request.TenantId, roles);
        }

        return roles;
    }
}