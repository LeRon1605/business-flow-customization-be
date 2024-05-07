using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using BusinessFlow.Application.UseCases.BusinessFlows.Commands;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Application.UseCases.BusinessFlows.Queries;
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

    [HttpGet("{id:int}/business-flows")]
    [ProducesResponseType(typeof(List<BusinessFlowVersionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceBusinessFlows([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetSpaceBusinessFlowVersionQuery(id));
        return Ok(result);
    }

    [HttpGet("{id:int}/business-flows/{businessFlowId:int}")]
    [ProducesResponseType(typeof(BusinessFlowDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceBusinessFlow([FromRoute] int id, [FromRoute] int businessFlowId)
    {
        var result = await _mediator.Send(new GetSpaceBusinessFlowQuery(businessFlowId, id));
        return Ok(result);
    }
    
    [HttpPut("{id:int}/business-flows")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSpaceBusinessFlow([FromRoute] int id, [FromBody] UpdateBusinessFlowRequestDto dto)
    {
        var businessFlowId = await _mediator.Send(new UpdateSpaceBusinessFlowCommand(id, dto.Blocks, dto.Branches));
        return Ok(SimpleIdResponse<int>.Create(businessFlowId));
    }
}