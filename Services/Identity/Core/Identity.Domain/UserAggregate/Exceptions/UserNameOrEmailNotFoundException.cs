using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class UserNameOrEmailNotFoundException : ResourceNotFoundException
{
    public UserNameOrEmailNotFoundException(string userNameOrEmail) : base($"User with username or email '{userNameOrEmail}' does not exist!", ErrorCodes.UserNameOrEmailNotFound)
    {
    }
}