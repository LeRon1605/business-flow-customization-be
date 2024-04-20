using System.Linq.Expressions;
using Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantDetailDto : TenantDto, IProjection<Tenant, int, TenantDetailDto>
{
    public int NumberOfStaff { get; set; }
    
    public Expression<Func<Tenant, TenantDetailDto>> GetProject()
    {
        return _ => new TenantDetailDto()
        {
            Id = _.Id,
            Name = _.Name,
            AvatarUrl = _.AvatarUrl,
            NumberOfStaff = _.Users.Count
        };
    }
}