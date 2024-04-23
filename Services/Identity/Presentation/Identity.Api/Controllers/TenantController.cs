using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Tenants.Commands;
using Identity.Application.UseCases.Tenants.Dtos;
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
    
    [HttpGet("{id}/users")]
    // [HasPermission(AppPermission.Tenants.View)]
    [ProducesResponseType(typeof(PagedResultDto<UserBasicInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersInTenantAsync([FromRoute] int id, [FromQuery] UserRequestDto dto)
    {
        var tenants = await _mediator.Send(new GetAllUsersInTenantQuery(id, dto.Search, dto.Page, dto.Size, dto.Sorting));
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