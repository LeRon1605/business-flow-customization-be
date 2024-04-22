using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Tenant.Api.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantController : ControllerBase
{
    private readonly IMediator _mediator;
    
    [HttpGet("get-all-users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersOfTenantAsync()
    {
        return Ok();
    }
}