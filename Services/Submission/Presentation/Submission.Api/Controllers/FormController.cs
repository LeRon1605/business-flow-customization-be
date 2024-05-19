using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.UseCases.Dtos;
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
}