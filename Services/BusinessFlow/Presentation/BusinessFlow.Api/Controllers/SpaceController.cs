using Application.Dtos.Spaces;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Presentation.Authorization;
using BusinessFlow.Application.UseCases.BusinessFlows.Commands;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Application.UseCases.BusinessFlows.Queries;
using BusinessFlow.Application.UseCases.Spaces.Commands;
using BusinessFlow.Application.UseCases.Spaces.Dtos;
using BusinessFlow.Application.UseCases.Spaces.Queries;
using BusinessFlow.Domain.SpaceAggregate.Specifications;
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
            , dto.BusinessFlow.Branches
            , dto.Form));
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

    [HttpGet("{id:int}/business-flows/out-comes")]
    [ProducesResponseType(typeof(List<BusinessFlowBlockOutComeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceBusinessFlowOutComes([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetLatestSpaceBusinessFlowOutComeQuery(id));
        return Ok(result);
    }


    [HttpPut("{id:int}/space-basic-info")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSpaceBasicInfo([FromRoute] int id, [FromBody] UpdateSpaceBasicInfoRequestDto dto)
    {
        await _mediator.Send(new UpdateSpaceBasicInfoCommand(id, dto.Name, dto.Description, dto.Color));
        return Ok();
    }
    
    [HttpPost("{id:int}/space-member")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSpaceMember([FromRoute] int id, [FromQuery] string userId)
    {
        await _mediator.Send(new AddNewMemberInSpaceCommand(id, userId));
        return Ok();
    }
    
    [HttpPut("{spaceId:int}/space-member")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRoleSpaceMember([FromRoute] int spaceId, [FromBody] SpaceMemberDto dto)
    {
        await _mediator.Send(new UpdateRoleSpaceMemberCommand(spaceId, dto.Id, dto.Role.Id));
        return Ok();
    }
    
    [HttpGet("{id:int}/list-space-members")]
    [ProducesResponseType(typeof(List<SpaceMemberDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceMembers([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetListMemberInSpaceQuery(id));
        return Ok(result);
    }
    
    [HttpDelete("{spaceId:int}/space-member/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMemberInSpace([FromRoute] int spaceId, [FromRoute] string userId)
    {
        await _mediator.Send(new DeleteMemberInSpaceCommand(spaceId, userId));
        return Ok();
    }
    
    [HttpDelete("{spaceId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSpace([FromRoute] int spaceId)
    {
        await _mediator.Send(new DeleteSpaceCommand(spaceId));
        return Ok();
    }
}