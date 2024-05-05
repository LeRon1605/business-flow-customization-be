using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using BusinessFlow.Application.UseCases.Spaces.Commands;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Application.UseCases.Spaces.Queries;
using Domain.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessFlow.Api.Controllers;

[ApiController]
[Route("api/spaces")]
public class SpaceController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SpaceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [HasPermission(AppPermission.Space.View)]
    [ProducesResponseType(typeof(List<SpaceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaces()
    {
        var spaces = await _mediator.Send(new GetTenantSpacesQuery());
        return Ok(spaces);
    }

    [HttpGet("{id}")]
    [HasPermission(AppPermission.Space.View)]
    [ProducesResponseType(typeof(SpaceDetailDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceDetail(int id)
    {
        var space = await _mediator.Send(new GetSpaceDetailQuery(id));
        return Ok(space);
    }

    [HttpPost]
    [HasPermission(AppPermission.Space.Create)]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSpace([FromBody] CreateSpaceRequestDto dto)
    {
        var spaceId = await _mediator.Send(new CreateSpaceCommand(dto.Name
            , dto.Description
            , dto.Color
            , dto.BusinessFlow.Blocks
            , dto.BusinessFlow.Branches));
        return Ok(SimpleIdResponse<int>.Create(spaceId));
    }
}