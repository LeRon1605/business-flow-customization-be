using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Enums;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantInvitationDto : IProjection<TenantInvitation, int, TenantInvitationDto>
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public TenantInvitationStatus Status { get; set; }
    public RoleDto Role { get; set; } = null!;
    public DateTime CreatedAt { get; set; }


    public Expression<Func<TenantInvitation, TenantInvitationDto>> GetProject()
    {
        return x => new TenantInvitationDto()
        {
            Id = x.Id,
            Email = x.Email,
            Status = x.Status,
            Role = new RoleDto()
            {
                Id = x.Role.Id,
                Name = x.Role.Name!
            },
            CreatedAt = x.Created
        };
    }
}