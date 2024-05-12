using Application.Dtos;
using Application.Dtos.Submissions.Identity;
using BuildingBlocks.Presentation.Authorization;
using Domain.Permissions;
using Identity.Application.UseCases.Permissions.Dtos;
using Identity.Application.UseCases.Permissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/permissions")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HasPermission(AppPermission.PermissionManagement.View)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedRoleAsync([FromQuery] PermissionFilterRequestDto dto)
    {
        var permissions = await _mediator.Send(new GetAllPermissionQuery(dto.Name));
        return Ok(permissions);
    }
}