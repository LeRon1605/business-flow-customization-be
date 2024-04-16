using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Identity.Application.UseCases.Users.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetAllUsersQuery : PagingAndSortingRequestDto, IQuery<PagedResultDto<UserBasicInfoDto>>
{
    public string? Search { get; set; }
    
    public GetAllUsersQuery(string? search
        , int page
        , int size
        , string? sorting) : base(page, size, sorting)
    {
        Search = search;
    }
}