using BuildingBlocks.Application.Dtos;
using Hub.Application.UseCases.Notifications.Commands;
using Hub.Application.UseCases.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetNotifications([FromQuery] PagingRequestDto dto)
    {
        var result = await _mediator.Send(new GetNotificationQuery(dto.Page, dto.Size));
        return Ok(result);
    }
    
    [HttpGet("unread-count")]
    [Authorize]
    public async Task<IActionResult> GetUnreadCount()
    {
        var result = await _mediator.Send(new GetUnReadNotificationCountQuery());
        return Ok(result);
    }
    
    [HttpPut("{id}/mark-read")]
    [Authorize]
    public async Task<IActionResult> MarkRead(Guid id)
    {
        await _mediator.Send(new MarkReadNotificationCommand(id));
        return NoContent();
    }
    
    [HttpPut("mark-all-read")]
    [Authorize]
    public async Task<IActionResult> MarkAllRead()
    {
        await _mediator.Send(new MarkAllReadNotificationCommand());
        return Ok();
    }
}