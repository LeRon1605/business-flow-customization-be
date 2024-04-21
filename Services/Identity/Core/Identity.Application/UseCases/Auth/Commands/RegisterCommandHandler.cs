using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Domain.Identities;
using Domain.Roles;
using Identity.Application.Services.Interfaces;

namespace Identity.Application.UseCases.Auth.Commands;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ITenantService _tenantService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    
    public RegisterCommandHandler(IIdentityService identityService
        , ITenantService tenantService
        , IUnitOfWork unitOfWork
        , ICurrentUser currentUser)
    {
        _identityService = identityService;
        _tenantService = tenantService;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }
    
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var user = await _identityService.CreateUserAsync(
                request.FullName,
                request.Email,
                AppUser.DefaultAvatar,
                request.Email,
                request.Password);
            if (!user.IsSuccess)
            {
                throw new ResourceInvalidOperationException(user.Error!, user.ErrorCode!);
            }

            _currentUser.Id = user.Data!.Id;
            
            var tenant = await _tenantService.CreateAsync(request.TenantName, AppTenant.DefaultAvatar);
            if (!tenant.IsSuccess)
            {
                throw new ResourceInvalidOperationException(tenant.Error!, tenant.ErrorCode!);
            }
            
            var grantRoleResult = await _identityService.GrantToRoleAsync(user.Data!.Id
                , AppRole.Admin
                , tenant.Data!.Id);
            if (!grantRoleResult.IsSuccess)
            {
                throw new ResourceInvalidOperationException(grantRoleResult.Error!, grantRoleResult.ErrorCode!);
            }
            
            await _tenantService.AddUserToTenantAsync(user.Data!.Id, true, tenant.Data!.Id);
            
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}