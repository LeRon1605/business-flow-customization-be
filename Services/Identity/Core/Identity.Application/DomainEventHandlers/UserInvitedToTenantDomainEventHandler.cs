using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.EventBus.Abstracts;
using Identity.Application.Services.Dtos;
using Identity.Domain.TenantAggregate.DomainEvents;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using IntegrationEvents.Hub;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Identity.Application.DomainEventHandlers;

public class UserInvitedToTenantDomainEventHandler : IDomainEventHandler<UserInvitedToTenantDomainEvent>
{
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ICurrentUser _currentUser;
    private readonly TenantInvitationSetting _tenantInvitationSetting;
    private readonly ILogger<UserInvitedToTenantDomainEventHandler> _logger;
    
    public UserInvitedToTenantDomainEventHandler(IBasicReadOnlyRepository<Tenant, int> tenantRepository
        , IEventPublisher eventPublisher
        , ICurrentUser currentUser
        , TenantInvitationSetting tenantInvitationSetting
        , ILogger<UserInvitedToTenantDomainEventHandler> logger)
    {
        _tenantRepository = tenantRepository;
        _eventPublisher = eventPublisher;
        _currentUser = currentUser;
        _tenantInvitationSetting = tenantInvitationSetting;
        _logger = logger;
    }
    
    public async Task Handle(UserInvitedToTenantDomainEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(notification.Invitation.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(notification.Invitation.TenantId);
        }
        
        await _eventPublisher.Publish(new EmailSenderIntegrationEvent($"Bạn được mời tham gia vào {tenant.Name}"
                , notification.Invitation.Email
                , "UserInvitedToTenant"
                , new
                {
                    Email = notification.Invitation.Email,
                    FullName = _currentUser.Name,
                    TenantName = tenant.Name,
                    TenantAvatarUrl = tenant.AvatarUrl,
                    CallBackUrl = QueryHelpers.AddQueryString(_tenantInvitationSetting.CallBackUrl, new Dictionary<string, string>()
                    {
                        { "Token", notification.Invitation.Token }
                    })
                })
            , cancellationToken);
    }
}