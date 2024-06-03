using Application.Dtos.Notifications.Requests;
using BuildingBlocks.Presentation.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.UseCases.Submissions.Queries;

namespace Submission.Api.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("submissions/data")]
    [InternalApi]
    public async Task<IActionResult> GetSubmissionNotificationData([FromBody] GetSubmissionNotificationDataRequestDto dto)
    {
        var submissions = await _mediator.Send(new GetSubmissionNotificationDataQuery(dto.SubmissionIds));
        return Ok(submissions);
    }
}