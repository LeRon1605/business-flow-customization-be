using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Domain.TenantAggregate.Exceptions;

public class TenantNotFoundException : ResourceNotFoundException
{
    public TenantNotFoundException(int id) : base(nameof(Tenant), id, ErrorCodes.TenantNotFound)
    {
    }
}