using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Tenants.Commands;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Application.UseCases.Tenants.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HasPermission(AppPermission.Tenants.View)]
    [ProducesResponseType(typeof(TenantDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantInfoAsync()
    {
        var tenant = await _mediator.Send(new GetCurrentTenantInfoQuery());
        return Ok(tenant);
    }

    [HttpPut]
    [HasPermission(AppPermission.Tenants.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateTenantInfoAsync([FromBody] TenantUpdateDto dto)
    {
        await _mediator.Send(new UpdateTenantInfoCommand(dto.Name, dto.AvatarUrl));
        return NoContent();
    }
    
    [HttpPost("invitations")]
    [HasPermission(AppPermission.Tenants.InviteMember)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> InviteMemberAsync([FromBody] TenantInvitationCreateDto createDto)
    {
        await _mediator.Send(new InviteTenantMemberCommand(createDto.Email, createDto.RoleId));
        return NoContent();
    }
    
    [HttpGet("invitations")]
    [HasPermission(AppPermission.Tenants.InviteMember)]
    [ProducesResponseType(typeof(PagedResultDto<TenantInvitationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInvitationsAsync([FromQuery] TenantInvitationRequestDto dto)
    {
        var invitations = await _mediator.Send(new GetTenantInvitationQuery(dto.Page
            , dto.Size
            , dto.Sorting
            , dto.Search));
        return Ok(invitations);
    }
}