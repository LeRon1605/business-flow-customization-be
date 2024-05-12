using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.UseCases.Dtos;
using Submission.Application.UseCases.Forms.Commands;
using Submission.Application.UseCases.Forms.Queries;
using Submission.Application.UseCases.Submissions.Commands;
using Submission.Application.UseCases.Submissions.Dtos;

namespace Submission.Api.Controllers;

[ApiController]
[Route("api/spaces")]
public class SpaceController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SpaceController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{spaceId}/submissions")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SubmitForm(int spaceId, [FromBody] SubmitFormDto data)
    {
        var submitId = await _mediator.Send(new SubmitFormCommand(spaceId, data));
        return Ok(SimpleIdResponse<int>.Create(submitId));
    }

    [HttpGet("{spaceId}/form-versions")]
    [ProducesResponseType(typeof(List<FormVersionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaceFormVersions(int spaceId)
    {
        var result = await _mediator.Send(new GetSpaceFormVersionQuery(spaceId));
        return Ok(result);
    }
    
    [HttpPost("{spaceId}/forms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateForm(int spaceId, [FromBody] FormRequestDto data)
    {
        await _mediator.Send(new CreateFormCommand(spaceId, data));
        return Ok();
    }
    
    [HttpPut("{spaceId}/forms")]
    [ProducesResponseType(typeof(SimpleIdResponse<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateForm(int spaceId, [FromBody] FormRequestDto data)
    {
        var versionId = await _mediator.Send(new UpdateFormCommand(spaceId, data));
        return Ok(SimpleIdResponse<int>.Create(versionId));
    }
    
    [HttpGet("{spaceId:int}/forms/latest")]
    [ProducesResponseType(typeof(FormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLatestForm(int spaceId)
    {
        var form = await _mediator.Send(new GetLatestSpaceFormQuery(spaceId));
        return Ok(form);
    }
    
    [HttpGet("{spaceId:int}/forms/{versionId:int}")]
    [ProducesResponseType(typeof(FormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForm(int spaceId, int versionId)
    {
        var form = await _mediator.Send(new GetFormVersionQuery(spaceId, versionId));
        return Ok(form);
    }
}