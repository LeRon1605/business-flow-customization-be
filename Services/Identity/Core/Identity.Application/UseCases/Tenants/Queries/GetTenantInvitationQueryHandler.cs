using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Specifications;

namespace Identity.Application.UseCases.Tenants.Queries;

public class GetTenantInvitationQueryHandler : IQueryHandler<GetTenantInvitationQuery, PagedResultDto<TenantInvitationDto>>
{
    private readonly IBasicReadOnlyRepository<TenantInvitation, int> _tenantInvitationRepository;
    private readonly ICurrentUser _currentUser;
    
    public GetTenantInvitationQueryHandler(IBasicReadOnlyRepository<TenantInvitation, int> tenantInvitationRepository
        , ICurrentUser currentUser)
    {
        _tenantInvitationRepository = tenantInvitationRepository;
        _currentUser = currentUser;
    }
    
    public async Task<PagedResultDto<TenantInvitationDto>> Handle(GetTenantInvitationQuery request, CancellationToken cancellationToken)
    {
        var specification = new InvitationByTenantSpecification(request.Page
            , request.Size
            , request.Sorting
            , request.Search
            , _currentUser.TenantId);
        
        var invitations = await _tenantInvitationRepository.GetPagedListAsync(specification, new TenantInvitationDto());
        var total = await _tenantInvitationRepository.GetCountAsync(specification);

        return new PagedResultDto<TenantInvitationDto>(total, request.Size, invitations);
    }
}