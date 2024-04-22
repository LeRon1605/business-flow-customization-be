using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Identity.Application.UseCases.Users.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetAllUsersInTenantQuery : PagingAndSortingRequestDto, IQuery<PagedResultDto<UserBasicInfoDto>>
{
    public int Id { get; set; } 
    public string? Search { get; set; }
    
    public GetAllUsersInTenantQuery(
        int id,
        string? search,
        int page,
        int size,
        string? sorting) : base(page, size, sorting)
    {
        Id = id;
        Search = search;
    } 
}