using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.Services.Interfaces;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.DomainEvents;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Identity.Domain.UserAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Identity.Application.DomainEventHandlers;

public class UserAcceptedTenantInvitationDomainEventHandler : IDomainEventHandler<UserAcceptedTenantInvitationDomainEvent>
{
    private readonly ITenantService _tenantService;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IBasicReadOnlyRepository<ApplicationRole, string> _roleRepository;
    private readonly ILogger<UserAcceptedTenantInvitationDomainEventHandler> _logger;
    
    public UserAcceptedTenantInvitationDomainEventHandler(ITenantService tenantService
        , IUserRepository userRepository
        , IIdentityService identityService
        , IBasicReadOnlyRepository<ApplicationRole, string> roleRepository
        , ILogger<UserAcceptedTenantInvitationDomainEventHandler> logger)
    {
        _tenantService = tenantService;
        _userRepository = userRepository;
        _identityService = identityService;
        _roleRepository = roleRepository;
        _logger = logger;
    }
    
    public async Task Handle(UserAcceptedTenantInvitationDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(new UserByEmailSpecification(notification.Invitation.Email));
        if (user == null)
        {
            throw new UserNotFoundException(notification.Invitation.Email);
        }
        
        var role = await _roleRepository.FindByIdAsync(notification.Invitation.RoleId);
        if (role == null)
        {
            throw new RoleNotFoundException(notification.Invitation.RoleId);
        }
        
        await _tenantService.AddUserToTenantAsync(user.Id, false, notification.Invitation.TenantId);
        
        var grantRoleResult = await _identityService.GrantToRoleAsync(user.Id
            , role.Name!
            , notification.Invitation.TenantId);
        if (!grantRoleResult.IsSuccess)
        {
            throw new ResourceInvalidOperationException(grantRoleResult.Error!, grantRoleResult.ErrorCode!);
        }
        
        _logger.LogInformation("User {Email} has been added to tenant {TenantId}", notification.Invitation.Email, notification.Invitation.TenantId);
    }
}