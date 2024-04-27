using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Identity.Domain.RoleAggregate;
using Identity.Domain.RoleAggregate.Exceptions;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Tenants.Commands;

public class InviteTenantMemberCommandHandler : ICommandHandler<InviteTenantMemberCommand>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InviteTenantMemberCommandHandler> _logger;
    
    public InviteTenantMemberCommandHandler(ITenantRepository tenantRepository
        , IRoleRepository roleRepository
        , ICurrentUser currentUser
        , IUnitOfWork unitOfWork
        , ILogger<InviteTenantMemberCommandHandler> logger)
    {
        _tenantRepository = tenantRepository;
        _roleRepository = roleRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(InviteTenantMemberCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId
            , $"{nameof(Tenant.Users)}.{nameof(UserInTenant.User)}");
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        var isRoleExisted = await _roleRepository.IsExistingAsync(request.RoleId);
        if (!isRoleExisted)
        {
            throw new RoleNotFoundException(request.RoleId);
        }
        
        tenant.InviteUser(request.Email, request.RoleId);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("User {Email} has been invited to tenant {TenantId}", request.Email, _currentUser.TenantId);
    }
}