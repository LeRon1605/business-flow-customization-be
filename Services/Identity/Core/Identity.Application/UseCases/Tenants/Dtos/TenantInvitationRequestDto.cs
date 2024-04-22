using BuildingBlocks.Application.Dtos;

namespace Identity.Application.UseCases.Tenants.Dtos;

public class TenantInvitationRequestDto : PagingAndSortingRequestDto
{
    public string? Search { get; set; }
}