using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class SpaceDto : IProjection<Space, int, SpaceDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
    
    public Expression<Func<Space, SpaceDto>> GetProject()
    {
        return x => new SpaceDto()
        {
            Id = x.Id,
            Name = x.Name,
            Color = x.Color
        };
    }
}