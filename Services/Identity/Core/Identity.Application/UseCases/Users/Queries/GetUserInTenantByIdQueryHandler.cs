using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using Identity.Application.UseCases.Users.Dtos;
using Identity.Domain.TenantAggregate.Entities;
using Identity.Domain.TenantAggregate.Exceptions;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using Identity.Domain.UserAggregate.Exceptions;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserInTenantByIdQueryHandler: IQueryHandler<GetUserInTenantByIdQuery, UserDto>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserRepository _userRepository;
    
    public GetUserInTenantByIdQueryHandler(IUserRepository userRepository, ICurrentUser currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }
    
    public async Task<UserDto> Handle(GetUserInTenantByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        var userInfo = new UserDto()
        {
            CurrentTenantId = _currentUser.TenantId,
            Id = user.Id,
            UserName = user.UserName!,
            FullName = user.FullName,
            Email = user.Email!,
            AvatarUrl = user.AvatarUrl,
            Role = user.GetDefaultRole(_currentUser.TenantId)
        };

        return userInfo;
    }
}