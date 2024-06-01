using Application.Dtos.SubmissionExecutions;
using BusinessFlow.Application.UseCases.BusinessFlows.Commands;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Application.UseCases.BusinessFlows.Queries;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessFlow.Api.Controllers;

[ApiController]
[Route("api/business-flows")]
public class BusinessFlowController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BusinessFlowController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("validate")]
    [ProducesResponseType(typeof(List<BusinessFlowBlockValidationModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateBusinessFlow([FromBody] ValidateBusinessFlowRequestDto requestDto)
    {
        var result = await _mediator.Send(new ValidateBusinessFlowCommand(requestDto.Blocks, requestDto.Branches));
        return Ok(result);
    }
    
    [HttpGet("submissions/{submissionId:int}")]
    [ProducesResponseType(typeof(List<BusinessFlowDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubmissionExecutionBusinessFlow([FromRoute] int submissionId)
    {
        var result = await _mediator.Send(new GetSubmissionExecutionBusinessFlowQuery(submissionId));
        return Ok(result);
    }
    
    [HttpPut("executions/{executionId:int}/tasks/{taskId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateExecutionTaskStatus([FromRoute] int executionId
        , [FromRoute] int taskId
        , [FromBody] UpdateExecutionTaskStatusRequestDto requestDto)
    {
        await _mediator.Send(new UpdateExecutionTaskStatusCommand(taskId, executionId, requestDto.Status));
        return Ok();
    }
    
    [HttpPost("submissions/{submissionId:int}/outcomes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SelectBusinessFlowOutCome([FromRoute] int submissionId
        , [FromBody] SelectBusinessFlowOutComeRequestDto requestDto)
    {
        await _mediator.Send(new SelectBusinessFlowOutComeCommand(submissionId, requestDto.OutComeId));
        return Ok();
    }
    
    [HttpGet("in-charge-executions")]
    [ProducesResponseType(typeof(List<AssignedSubmissionExecutionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAssignedSubmissionExecutions()
    {
        var result = await _mediator.Send(new GetAssignedSubmissionExecutionQuery());
        return Ok(result);
    }
}