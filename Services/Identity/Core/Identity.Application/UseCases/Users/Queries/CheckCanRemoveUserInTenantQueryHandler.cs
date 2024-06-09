using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using Domain.Roles;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Users.Queries;

public class CheckCanRemoveUserInTenantQueryHandler : IQueryHandler<CheckCanRemoveUserInTenantQuery, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;
    
    public CheckCanRemoveUserInTenantQueryHandler(IUserRepository userRepository, ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }
    
    public async Task<bool> Handle(CheckCanRemoveUserInTenantQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }
        if (_currentUser.Id == request.Id || user.GetDefaultRole(_currentUser.TenantId) == AppRole.Admin)
        {
            return false;
        }
        return true;
    }
}