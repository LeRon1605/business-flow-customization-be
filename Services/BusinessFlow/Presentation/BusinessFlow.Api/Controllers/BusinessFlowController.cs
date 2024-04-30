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
    [ProducesResponseType(typeof(List<BusinessFlowBlockValidationModel<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateBusinessFlow([FromBody] ValidateBusinessFlowDto dto)
    {
        var result = await _mediator.Send(new ValidateBusinessFlowCommand(dto.Blocks, dto.Branches));
        return Ok(result);
    }
}