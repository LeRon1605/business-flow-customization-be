using BuildingBlocks.Domain.Exceptions.Resources;
using ApplicationUser = Identity.Domain.UserAggregate.Entities.ApplicationUser;

namespace Identity.Domain.UserAggregate.Exceptions;

public class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(string id) 
        : base("User", nameof(ApplicationUser.Id), id, ErrorCodes.UserNotFound)
    {
    }
}