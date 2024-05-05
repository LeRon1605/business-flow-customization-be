using System.Linq.Expressions;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class SpaceMemberDto
{
    public string Id { get; set; } = null!;
    public SpaceRoleDto Role { get; set; } = null!;
}