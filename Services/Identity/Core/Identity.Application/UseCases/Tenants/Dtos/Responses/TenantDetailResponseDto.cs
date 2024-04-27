using System.Linq.Expressions;
using Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using Identity.Domain.TenantAggregate.Entities;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantDetailResponseDto : TenantDto, IProjection<Tenant, int, TenantDetailResponseDto>
{
    public int NumberOfStaff { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Expression<Func<Tenant, TenantDetailResponseDto>> GetProject()
    {
        return _ => new TenantDetailResponseDto()
        {
            Id = _.Id,
            Name = _.Name,
            AvatarUrl = _.AvatarUrl,
            NumberOfStaff = _.Users.Count,
            CreatedAt = _.Created
        };
    }
}