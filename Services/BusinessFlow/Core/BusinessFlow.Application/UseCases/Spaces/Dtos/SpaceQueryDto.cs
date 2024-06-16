using System.Linq.Expressions;
using Application.Dtos.Spaces;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class SpaceQueryDto : SpaceDto, IProjection<Space, int, SpaceDto>
{
    public Expression<Func<Space, SpaceDto>> GetProject()
    {
        return space => new SpaceDto
        {
            Id = space.Id,
            Color = space.Color,
            Name = space.Name
        };
    }
}