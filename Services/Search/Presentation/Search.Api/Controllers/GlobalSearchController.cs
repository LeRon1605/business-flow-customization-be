using MediatR;
using Microsoft.AspNetCore.Mvc;
using Search.Application.UseCases.Dtos;
using Search.Application.UseCases.Queries;

namespace Search.Api.Controllers;

[ApiController]
[Route("api/global")]
public class GlobalSearchController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public GlobalSearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(GlobalSearchQueryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GlobalSearch([FromQuery] string searchTerm, [FromQuery] int page, [FromQuery] int size)
    {
        var result = await _mediator.Send(new GlobalSearchQuery(searchTerm, page, size));
        return Ok(result);
    }
}