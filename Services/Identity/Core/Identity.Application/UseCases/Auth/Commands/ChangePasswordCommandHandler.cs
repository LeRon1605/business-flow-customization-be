using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Application.UseCases.Auth.Commands;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<ChangePasswordCommandHandler> _logger;
    
    public ChangePasswordCommandHandler(ICurrentUser currentUser
        , UserManager<ApplicationUser> userManager
        , ILogger<ChangePasswordCommandHandler> logger)
    {
        _currentUser = currentUser;
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.Id);
        if (user == null)
        {
            throw new UserNotFoundException(_currentUser.Id);
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }
}