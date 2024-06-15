using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Domain.Roles;
using Identity.Application.Services.Interfaces;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Users.Commands;

public class RemoveUserFromTenantCommandHandler : ICommandHandler<RemoveUserFromTenantCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantService _tenantService;
    private readonly ILogger<RemoveUserFromTenantCommandHandler> _logger;
    
    public RemoveUserFromTenantCommandHandler(
        ICurrentUser currentUser,
        ITenantRepository tenantRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITenantService tenantService,
        ILogger<RemoveUserFromTenantCommandHandler> logger)
    {
        _currentUser = currentUser;
        _tenantRepository = tenantRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tenantService = tenantService;
        _logger = logger;
    }
    
    public async Task Handle(RemoveUserFromTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        var user = await _userRepository.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }
        if (user.GetDefaultRole(_currentUser.TenantId) == request.UserId || user.GetDefaultRole(_currentUser.TenantId) == AppRole.Admin)
        {
            _logger.LogWarning("Cannot remove user from tenant");
            return;
        }
        await _tenantService.RemoveUserFromTenantAsync(user.Id, tenant.Id);
    }
}