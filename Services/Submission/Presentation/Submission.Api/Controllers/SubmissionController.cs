using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.UseCases.Submissions.Commands;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Application.UseCases.Submissions.Queries;

namespace Submission.Api.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubmissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("{submissionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateName(int submissionId, [FromBody] UpdateSubmitFormNameDto dto)
    {
        await _mediator.Send(new UpdateSubmitFormNameCommand(submissionId, dto.Name));
        return Ok();
    }
    
    [HttpPut("{submissionId:int}/fields")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateField(int submissionId, [FromBody] UpdateSubmitFormFieldDto dto)
    {
        await _mediator.Send(new UpdateSubmitFormFieldCommand(submissionId, dto.Field));
        return Ok();
    }
    
    [HttpGet("executions/{executionId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExecutionSubmission(int executionId)
    {
        var submission = await _mediator.Send(new GetExecutionSubmissionQuery(executionId));
        return Ok(submission);
    }
    
    [HttpGet("data")]
    public async Task<IActionResult> GetSubmissions([FromQuery] List<int> submissionIds)
    {
        var result = await _mediator.Send(new GetSubmissionDataQuery(submissionIds));
        return Ok(result);
    }
    
    [HttpPost("external-submit")]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalSubmit([FromBody] ExternalSubmitFormDto dto)
    {
        var id = await _mediator.Send(new SubmitFormExternalCommand(dto));
        return Ok(SimpleIdResponse<int>.Create(id));
    }
}