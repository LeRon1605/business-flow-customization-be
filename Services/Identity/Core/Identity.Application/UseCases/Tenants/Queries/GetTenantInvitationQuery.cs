using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Application.UseCases.Tenants.Dtos.Responses;

namespace Identity.Application.UseCases.Tenants.Queries;

public class GetTenantInvitationQuery : PagingAndSortingRequestDto, IQuery<PagedResultDto<TenantInvitationResponseDto>>
{
    public string? Search { get; set; }
    
    public GetTenantInvitationQuery(int page, int size, string? sorting, string? search) : base(page, size, sorting)
    {
        Search = search;
    }
}