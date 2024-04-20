using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Identity.Domain.TenantAggregate;
using Identity.Domain.TenantAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Tenants.Commands;

public class UpdateTenantInfoCommandHandler : ICommandHandler<UpdateTenantInfoCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly ITenantRepository _tenantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateTenantInfoCommandHandler> _logger;
    
    public UpdateTenantInfoCommandHandler(ICurrentUser currentUser
        , ITenantRepository tenantRepository
        , IUnitOfWork unitOfWork
        , ILogger<UpdateTenantInfoCommandHandler> logger)
    {
        _currentUser = currentUser;
        _tenantRepository = tenantRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(UpdateTenantInfoCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId);
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }
        
        tenant.Update(request.Name, request.AvatarUrl);
        await _unitOfWork.CommitAsync();
        
        _logger.LogInformation("Updated info of tenant {TenantId}", _currentUser.TenantId);
    }
}