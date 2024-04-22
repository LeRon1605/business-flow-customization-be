using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.TenantAggregate.Exceptions;

public class TenantInvitationAlreadyExistedException : ResourceAlreadyExistException
{
    public TenantInvitationAlreadyExistedException(string email) : base($"Already invited {email}!", ErrorCodes.TenantInvitationAlreadyExisted)
    {
    }
}