using BuildingBlocks.Application.Attributes;
using BuildingBlocks.Application.Dtos;
using Identity.Domain.RoleAggregate.Entities;

namespace Identity.Application.UseCases.Roles.Dtos;

public class RoleFilterAndPagingRequestDto : PagingAndSortingRequestDto
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(ApplicationRole.Name)}")]
    public override string Sorting { get; set; } = string.Empty;
}