using Application.Dtos;
using Domain;
using Identity.Application.UseCases.Auth.Commands;
using Identity.Application.UseCases.Auth.Dtos;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    // [HasPermission(AppPermissions.Users.ViewProfile)]
    [Authorize]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfileAsync()
    {
        var userInfo = await _mediator.Send(new GetUserInfoQuery());
        return Ok(userInfo);
    }
    
    [HttpPost("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto dto)
    {
        await _mediator.Send(new ChangePasswordCommand(dto.CurrentPassword, dto.NewPassword));
        return NoContent();
    }
}