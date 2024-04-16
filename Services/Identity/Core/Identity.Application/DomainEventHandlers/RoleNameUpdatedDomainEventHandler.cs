using Application.Caching.Interfaces;
using BuildingBlocks.Domain.Events;
using Identity.Domain.RoleAggregate.DomainEvents;
using Microsoft.Extensions.Logging;

namespace Identity.Application.DomainEventHandlers;

public class RoleNameUpdatedDomainEventHandler : IDomainEventHandler<RoleNameUpdatedDomainEvent>
{
    private readonly IPermissionCacheManager _permissionCacheManager;
    private readonly ILogger<PermissionForRoleUpdatedDomainEventHandler> _logger;
    
    public RoleNameUpdatedDomainEventHandler(
        IPermissionCacheManager permissionCacheManager,
        ILogger<PermissionForRoleUpdatedDomainEventHandler> logger)
    {
        _permissionCacheManager = permissionCacheManager;
        _logger = logger;
    }
    
    public async Task Handle(RoleNameUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Cache] Remove permission cache data for role {Role}!", notification.Name);
        
        await _permissionCacheManager.RemoveForRoleAsync(notification.Name);
    }
}