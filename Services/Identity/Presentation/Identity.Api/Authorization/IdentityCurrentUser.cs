using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using BuildingBlocks.Presentation.Authorization;
using Identity.Application.UseCases.Permissions.Queries;
using Identity.Application.UseCases.Roles.Queries;
using MediatR;

namespace Identity.Api.Authorization;

public class IdentityCurrentUser : CurrentUser, ICurrentUser
{
    private readonly IMediator _mediator;
    public IdentityCurrentUser(IHttpContextAccessor httpContextAccessor, IMediator mediator) : base(httpContextAccessor)
    {
        _mediator = mediator;
    }

    public override async Task<List<string>> GetRolesAsync()
    {
        if (IsAuthenticated)
        {
            var roles = await _mediator.Send(new GetAllRoleForUserQuery(Id, TenantId));
            return roles.ToList();   
        }

        throw new ResourceUnauthorizedAccessException("User is not authenticated");
    }
    
    public override async Task<List<string>> GetPermissionsAsync()
    {
        if (IsAuthenticated)
        {
            var permissions = await _mediator.Send(new GetAllPermissionForUserQuery(Id, TenantId));
            return permissions.Select(x => x.Name).ToList();
        }

        throw new ResourceUnauthorizedAccessException("User is not authenticated");
    }
}