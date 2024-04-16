using BuildingBlocks.Domain.Repositories;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Domain.TenantAggregate;

public interface ITenantRepository : IRepository<Tenant, int>
{
    Task<IList<Tenant>> FindByUserAsync(string userId);
}