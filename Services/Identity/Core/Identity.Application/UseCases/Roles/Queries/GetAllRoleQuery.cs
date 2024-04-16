using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Identity.Application.UseCases.Roles.Dtos;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetAllRoleQuery : PagingAndSortingRequestDto, IQuery<PagedResultDto<RoleDto>>
{
    public string Name { get; set; }

    public GetAllRoleQuery(int page, int size, string sorting, string name) : base(page, size, sorting)
    {
        Name = name;
    }
}