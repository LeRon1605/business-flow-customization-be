using BuildingBlocks.Application.Dtos;

namespace Identity.Application.UseCases.Tenants.Dtos.Requests;

public class FilterAndPagingTenantInvitationRequestDto : PagingAndSortingRequestDto
{
    public string? Search { get; set; }
}