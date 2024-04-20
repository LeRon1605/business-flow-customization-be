using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;

namespace Identity.Application.UseCases.Tenants.Queries;

public class GetCurrentTenantInfoQueryHandler : IQueryHandler<GetCurrentTenantInfoQuery, TenantDetailDto>
{
    private readonly IBasicReadOnlyRepository<Tenant, int> _tenantRepository;
    private readonly ICurrentUser _currentUser;

    public GetCurrentTenantInfoQueryHandler(IBasicReadOnlyRepository<Tenant, int> tenantRepository
        , ICurrentUser currentUser)
    {
        _tenantRepository = tenantRepository;
        _currentUser = currentUser;
    }
    
    public async Task<TenantDetailDto> Handle(GetCurrentTenantInfoQuery request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.FindByIdAsync(_currentUser.TenantId, new TenantDetailDto());
        if (tenant == null)
        {
            throw new TenantNotFoundException(_currentUser.TenantId);
        }

        return tenant;
    }
}