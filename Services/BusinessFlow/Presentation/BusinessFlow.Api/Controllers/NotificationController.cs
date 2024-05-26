using Application.Dtos.Notifications.Requests;
using BuildingBlocks.Presentation.Authorization;
using BusinessFlow.Application.UseCases.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessFlow.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("business-flows/data")]
    [InternalApi]
    public async Task<IActionResult> GetSubmissionExecutionBusinessFlow([FromBody] GetBusinessFlowNotificationDataRequestDto dto)
    {
        var query = new GetBusinessFlowNotificationDataQuery(dto.SpaceId, dto.Entities);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}