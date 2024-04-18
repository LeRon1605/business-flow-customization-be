using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Exceptions.Resources;
using Domain;
using Identity.Application.Services.Interfaces;

namespace Identity.Application.UseCases.Auth.Commands;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ITenantService _tenantService;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterCommandHandler(IIdentityService identityService
        , ITenantService tenantService
        , IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _tenantService = tenantService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var tenant = await _tenantService.CreateAsync(request.TenantName, AppTenant.DefaultAvatar);
            var user = await _identityService.CreateUserAsync(
                request.FullName,
                request.Email,
                AppUser.DefaultAvatar,
                request.Email,
                request.Password);

            if (!tenant.IsSuccess || !user.IsSuccess)
            {
                var error = user.ErrorCode ?? tenant.ErrorCode;
                var message = user.Error ?? tenant.Error;
                throw new ResourceInvalidOperationException(message!, error!);
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