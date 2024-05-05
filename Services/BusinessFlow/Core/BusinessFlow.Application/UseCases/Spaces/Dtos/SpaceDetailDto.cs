using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class SpaceDetailDto : IProjection<Space, int, SpaceDetailDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Color { get; set; } = null!;
    public List<SpaceMemberDto> Members { get; set; } = null!;
    
    public Expression<Func<Space, SpaceDetailDto>> GetProject()
    {
        return x => new SpaceDetailDto()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Color = x.Color,
            Members = x.Members.Select(m => new SpaceMemberDto()
            {
                Id = m.UserId,
                Role = new SpaceRoleDto()
                {
                    Id = m.Role.Id,
                    Name = m.Role.Name
                }
            }).ToList()
        };
    }
}