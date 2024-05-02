using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Tenants.Commands;
using Identity.Application.UseCases.Tenants.Dtos;
using Identity.Application.UseCases.Tenants.Dtos.Requests;
using Identity.Application.UseCases.Tenants.Dtos.Responses;
using Identity.Application.UseCases.Tenants.Queries;
using Identity.Application.UseCases.Users.Commands;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Application.UseCases.Users.Queries;
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
    [ProducesResponseType(typeof(TenantDetailResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTenantInfoAsync()
    {
        var tenant = await _mediator.Send(new GetCurrentTenantInfoQuery());
        return Ok(tenant);
    }

    [HttpPut]
    [HasPermission(AppPermission.Tenants.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateTenantInfoAsync([FromBody] UpdateTenantRequestDto dto)
    {
        await _mediator.Send(new UpdateTenantInfoCommand(dto.Name, dto.AvatarUrl));
        return NoContent();
    }

    [HttpPost("invitations")]
    [HasPermission(AppPermission.Tenants.InviteMember)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> InviteMemberAsync([FromBody] InviteTenantMemberRequestDto createRequestDto)
    {
        await _mediator.Send(new InviteTenantMemberCommand(createRequestDto.Email, createRequestDto.RoleId));
        return NoContent();
    }
    
    [HttpGet("invitations")]
    [HasPermission(AppPermission.Tenants.InviteMember)]
    [ProducesResponseType(typeof(PagedResultDto<TenantInvitationResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInvitationsAsync([FromQuery] FilterAndPagingTenantInvitationRequestDto dto)
    {
        var invitations = await _mediator.Send(new GetTenantInvitationQuery(dto.Page
            , dto.Size
            , dto.Sorting
            , dto.Search));
        return Ok(invitations);
    }
    
    [HttpPost("invitations/accept")]
    [ProducesResponseType(typeof(AcceptTenantInvitationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> AcceptInvitationAsync([FromBody] AcceptTenantInvitationRequestDto dto)
    {
        var response = await _mediator.Send(new AcceptTenantInvitationCommand(dto.Token));
        return Ok(response);
    }

    [HttpPost("invitations/accept/account")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateInvitationAccountAsync(
        [FromBody] CreateAccountTenantInvitationRequestDto requestDto)
    {
        await _mediator.Send(new CreateAccountTenantInvitationCommand(requestDto.FullName, requestDto.Token, requestDto.Password));
        return NoContent();
    }
    

    [HttpGet("users")]
    // [HasPermission(AppPermission.Tenants.View)]
    [ProducesResponseType(typeof(PagedResultDto<UserBasicInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersInTenantAsync([FromQuery] UserRequestDto dto)
    {
        var tenants = await _mediator.Send(new GetAllUsersInTenantQuery(dto.Search, dto.Page, dto.Size, dto.Sorting));
        return Ok(tenants);
    }
    
    [HttpGet("user/{id}")]
    // [HasPermission(AppPermission.Tenants.View)]
    [ProducesResponseType(typeof(UserDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserInTenantByIdAsync(string id)
    {
        var user = await _mediator.Send(new GetUserInTenantByIdQuery(id));
        return Ok(user);
    }
    
    [HttpPut("remove-user")]
    // [HasPermission(AppPermission.Tenants.Edit)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveUserFromTenantAsync([FromBody] string id)
    {
        await _mediator.Send(new RemoveUserFromTenantCommand(id));
        return NoContent();
    }
}