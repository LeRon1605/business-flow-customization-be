using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Submission.Api.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionController : ControllerBase
{
    private IMediator _mediator;

    public SubmissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
}