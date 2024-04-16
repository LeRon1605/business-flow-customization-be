using BuildingBlocks.Domain.Repositories;
using Identity.Domain.PermissionAggregate.Entities;

namespace Identity.Domain.PermissionAggregate;

public interface IPermissionRepository : IRepository<ApplicationPermission, int>
{
    Task<IList<ApplicationPermission>> FilterAsync(string name);
}