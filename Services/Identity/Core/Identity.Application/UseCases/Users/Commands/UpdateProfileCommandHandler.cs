using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Exceptions.Resources;
using Identity.Application.Services.Interfaces;

namespace Identity.Application.UseCases.Users.Commands;

public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUser _currentUser;
    
    public UpdateProfileCommandHandler(IIdentityService identityService
        , ICurrentUser currentUser)
    {
        _identityService = identityService;
        _currentUser = currentUser;
    }
    
    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await _identityService.UpdateAsync(_currentUser.Id, request.FullName, request.AvatarUrl);
        if (!updateResult.IsSuccess)
        {
            throw new ResourceInvalidOperationException(updateResult.ErrorCode!, updateResult.Error!);
        }
    }
}