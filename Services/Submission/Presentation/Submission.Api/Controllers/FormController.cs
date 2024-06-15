using Application.Dtos.Spaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.UseCases.Dtos;
using Submission.Application.UseCases.Forms.Commands;
using Submission.Application.UseCases.Forms.Queries;

namespace Submission.Api.Controllers;

[Route("api/forms")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public FormController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("business-flows/{businessFlowBlockId:guid}")]
    [ProducesResponseType(typeof(FormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBusinessFlowBlockForm(Guid businessFlowBlockId)
    {
        var form = await _mediator.Send(new GetBusinessFlowBlockFormQuery(businessFlowBlockId));
        return Ok(form);
    }
    
    [HttpGet("spaces")]
    [ProducesResponseType(typeof(List<SpaceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpaces([FromQuery] List<int> spaceIds)
    {
        var spaces = await _mediator.Send(new GetSpacesFormQuery(spaceIds));
        return Ok(spaces);
    }
    
    [AllowAnonymous]
    [HttpGet("public-form/{token}")]
    [ProducesResponseType(typeof(FormDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublicForm(string token)
    {
        var form = await _mediator.Send(new GetPublicFormQuery(token));
        return Ok(form);
    }
}