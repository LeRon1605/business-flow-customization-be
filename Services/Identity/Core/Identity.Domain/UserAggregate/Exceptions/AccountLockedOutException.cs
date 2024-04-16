using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.UserAggregate.Exceptions;

public class AccountLockedOutException: ResourceAccessDeniedException
{
    public AccountLockedOutException() : base("This account has been locked out!", ErrorCodes.AccountLockedOut)
    {
    }
}