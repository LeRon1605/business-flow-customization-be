using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Presentation.Authorization;
using Identity.Application.UseCases.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("users/data")]
    [InternalApi]
    [ProducesResponseType(typeof(List<IdentityNotificationDataDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotificationsDataAsync([FromQuery] List<string> userIds)
    {
        var result = await _mediator.Send(new GetUserNotificationDataQuery(userIds));
        return Ok(result);
    }
}