using BuildingBlocks.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
}