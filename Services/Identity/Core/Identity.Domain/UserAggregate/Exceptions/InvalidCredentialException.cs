using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class InvalidCredentialException: ResourceNotFoundException
{
    public InvalidCredentialException() : base("Account with provided information does not exist!", ErrorCodes.InvalidCredential)
    {
    }
}