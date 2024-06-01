using Application.Dtos.Notifications.Models;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.EventBus.Abstracts;
using Domain.Enums;
using Identity.Application.Services.Interfaces;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.RoleAggregate.Exceptions;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.DomainEvents;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Identity.Domain.UserAggregate.Specifications;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace Identity.Application.DomainEventHandlers;

public class UserAcceptedTenantInvitationDomainEventHandler : IDomainEventHandler<UserAcceptedTenantInvitationDomainEvent>
{
    private readonly ITenantService _tenantService;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IBasicReadOnlyRepository<ApplicationRole, string> _roleRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly ILogger<UserAcceptedTenantInvitationDomainEventHandler> _logger;
    
    public UserAcceptedTenantInvitationDomainEventHandler(ITenantService tenantService
        , IUserRepository userRepository
        , IIdentityService identityService
        , IBasicReadOnlyRepository<ApplicationRole, string> roleRepository
        , IEventPublisher eventPublisher
        , IBasicReadOnlyRepository<Tenant, int> tenantRepository
        , ILogger<UserAcceptedTenantInvitationDomainEventHandler> logger)
    {
        _tenantService = tenantService;
        _userRepository = userRepository;
        _identityService = identityService;
        _roleRepository = roleRepository;
        _eventPublisher = eventPublisher;
        _tenantRepository = tenantRepository;
        _logger = logger;
    }
    
    public async Task Handle(UserAcceptedTenantInvitationDomainEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(notification.Invitation.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(notification.Invitation.TenantId);
        }
        
        var user = await _userRepository.FindAsync(new UserByEmailSpecification(notification.Invitation.Email));
        if (user == null)
        {
            throw new UserNotFoundException(notification.Invitation.Email);
        }
        
        await AddUserToTenantAsync(notification, user);
        await PushNotificationAsync(notification, user, tenant);
    }
    
    private async Task AddUserToTenantAsync(UserAcceptedTenantInvitationDomainEvent notification, ApplicationUser user)
    {
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
    
    private async Task PushNotificationAsync(UserAcceptedTenantInvitationDomainEvent notification
        , ApplicationUser user
        , Tenant tenant)
    {
        var integrationEvent = new NotificationIntegrationEvent(NotificationType.UserInvitationAccepted
            , new NotificationUserInvitationAcceptedModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                TenantId = tenant.Id,
                TenantName = tenant.Name
            }
            , new List<string>() { notification.Invitation.CreatedBy! });

        await _eventPublisher.Publish(integrationEvent);
    }
}