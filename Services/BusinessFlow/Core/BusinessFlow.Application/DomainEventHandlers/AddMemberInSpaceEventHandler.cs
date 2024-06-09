using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.EventBus.Abstracts;
using BusinessFlow.Domain.BusinessFlowAggregate.DomainEvents;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace BusinessFlow.Application.DomainEventHandlers;

public class AddMemberInSpaceEventHandler : IDomainEventHandler<AddMemberInSpaceEvent>
{
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly IBasicReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<AddMemberInSpaceEventHandler> _logger;
    
    public AddMemberInSpaceEventHandler(IBasicReadOnlyRepository<ApplicationUser, string> userRepository
        , IEventPublisher eventPublisher
        , ILogger<AddMemberInSpaceEventHandler> logger, ICurrentUser currentUser, IBasicReadOnlyRepository<Tenant, int> tenantRepository)
    {
        _userRepository = userRepository;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _currentUser = currentUser;
        _tenantRepository = tenantRepository;
    }
    
    public async Task Handle(AddMemberInSpaceEvent notification, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            _logger.LogWarning($"Tenant {_currentUser.TenantId} not found");
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        var user = await _userRepository.FindByIdAsync(notification.UserId);
        if (user == null)
        {
            _logger.LogWarning($"User {notification.UserId} not found");
            throw new UserNotFoundException(notification.UserId);
        }
        
        await _eventPublisher.Publish(new EmailSenderIntegrationEvent($"Bạn được thêm vào không gian {notification.SpaceName}"
                , user.Email
                , "AddMemberInSpace"
                , new
                {
                    FullName = user.FullName,
                    SpaceName = notification.SpaceName,
                    TenantName = tenant.Name,
                    RoleName = notification.Role,
                    CallBackUrl = "hehe"
                })
            , cancellationToken);
    }
}