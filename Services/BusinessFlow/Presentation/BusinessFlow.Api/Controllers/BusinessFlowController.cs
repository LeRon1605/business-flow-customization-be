using BusinessFlow.Application.UseCases.BusinessFlows.Commands;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessFlow.Api.Controllers;

[ApiController]
[Route("api/business-flows")]
public class BusinessFlowController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BusinessFlowController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("validate")]
    [ProducesResponseType(typeof(List<BusinessFlowBlockValidationModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateBusinessFlow([FromBody] ValidateBusinessFlowRequestDto requestDto)
    {
        var result = await _mediator.Send(new ValidateBusinessFlowCommand(requestDto.Blocks, requestDto.Branches));
        return Ok(result);
    }
}