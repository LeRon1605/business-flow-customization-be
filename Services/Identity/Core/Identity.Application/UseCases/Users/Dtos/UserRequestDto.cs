using BuildingBlocks.Application.Attributes;
using BuildingBlocks.Application.Dtos;
using Identity.Domain.UserAggregate.Entities;

namespace Identity.Application.UseCases.Users.Dtos;

public class UserRequestDto: PagingAndSortingRequestDto
{
    public string? Search { get; set; }

    [SortConstraint(Fields = $"{nameof(ApplicationUser.UserName)}, {nameof(ApplicationUser.Email)}")]
    public override string? Sorting { get; set; } = string.Empty;
}