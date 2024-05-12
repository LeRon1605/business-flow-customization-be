using BuildingBlocks.Application.Mappers;
using Identity.Domain.TenantAggregate.Entities;
using TenantDto = Application.Dtos.Submissions.Identity.TenantDto;

namespace Identity.Application.UseCases.Tenants.Mappers;

public class TenantMapper : MappingProfile
{
    public TenantMapper()
    {
        CreateMap<Tenant, TenantDto>();
    }
}