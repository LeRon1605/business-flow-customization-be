using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Permissions.Commands;
using Identity.Application.UseCases.Permissions.Dtos;
using Identity.Application.UseCases.Permissions.Queries;
using Identity.Application.UseCases.Roles.Commands;
using Identity.Application.UseCases.Roles.Dtos;
using Identity.Application.UseCases.Roles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HasPermission(AppPermission.Roles.Management)]
    [ProducesResponseType(typeof(PagedResultDto<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedRoleAsync([FromQuery] RoleFilterAndPagingRequestDto dto)
    {
        var roles = await _mediator.Send(new GetAllRoleQuery(dto.Page, dto.Size, dto.Sorting, dto.Name));
        return Ok(roles);
    }
    
    [HttpGet("{id}")]
    [HasPermission(AppPermission.Roles.View)]
    [ActionName(nameof(GetRoleByIdAsync))]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleByIdAsync(string id)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery(id));
        return Ok(role);
    }
    
    [HttpPost]
    [HasPermission(AppPermission.Roles.Create)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRoleAsync(RoleCreateDto dto)
    {
        var role = await _mediator.Send(new CreateRoleCommand(dto.Name));
        return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    [HasPermission(AppPermission.Roles.Edit)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRoleAsync(string id, RoleUpdateDto dto)
    {
        var role = await _mediator.Send(new UpdateRoleCommand(id, dto.Name));
        return Ok(role);
    }
    
    [HttpDelete("{id}")]
    [HasPermission(AppPermission.Roles.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRoleAsync(string id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));
        return NoContent();
    }
    
    [HttpGet("{id}/permissions")]
    [HasPermission(AppPermission.Roles.ViewPermission)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPermissionForRoleAsync(string id, [FromQuery] PermissionFilterRequestDto dto)
    {
        var permissions = await _mediator.Send(new GetAllPermissionForRoleQuery(id, dto.Name));
        return Ok(permissions);
    }
    
    [HttpPut("{id}/permissions")]
    [HasPermission(AppPermission.Roles.UpdatePermission)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePermissionForRoleAsync(string id, IEnumerable<int> permissionIds)
    {
        var permissions = await _mediator.Send(new UpdatePermissionForRoleCommand(id, permissionIds));
        return Ok(permissions);
    }
}