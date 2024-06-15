using Application.Dtos;
using Application.Dtos.Identity;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Permissions.Dtos;
using Identity.Application.UseCases.Permissions.Queries;
using Identity.Application.UseCases.Users.Commands;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/permissions")]
    [HasPermission(AppPermission.Users.ViewPermissions)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPermissionsForUserAsync(string id, [FromQuery] int tenantId)
    {
        var permissions = await _mediator.Send(new GetAllPermissionForUserQuery(id, tenantId));
        return Ok(permissions);
    }
    
    [HttpGet("{id}/tenants/{tenantId}/roles")]
    [HasPermission(AppPermission.Users.ViewRoles)]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRolesForUserAsync(string id, int tenantId)
    {
        var roles = await _mediator.Send(new GetRolesForUserQuery(id, tenantId));
        return Ok(roles);
    }
    
    [HttpPut("{id}/tenants/{tenantId}/roles")]
    [HasPermission(AppPermission.Users.EditRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateRolesForUserAsync(string id, int tenantId, [FromBody] IEnumerable<string> roleIds)
    {
        await _mediator.Send(new UpdateRolesForUserCommand(id, tenantId, roleIds));
        return NoContent();
    }
    
    [HttpGet]
    [HasPermission(AppPermission.Users.Management)]
    [ProducesResponseType(typeof(PagedResultDto<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserRequestDto dto)
    {
        var query = new GetAllUsersQuery(dto.Search, dto.Page, dto.Size, dto.Sorting);
        
        var users = await _mediator.Send(query);
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    [HasPermission(AppPermission.Users.View)]
    [ProducesResponseType(typeof(UserDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserByIdAsync(string id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }
    
}