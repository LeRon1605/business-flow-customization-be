using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HubController : ControllerBase
{
    private readonly ICurrentUser _currentUser;
    
    public HubController(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        var a = _currentUser;
        var roles = await _currentUser.GetRolesAsync();
        return Ok();
    }
}