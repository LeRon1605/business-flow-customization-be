using BuildingBlocks.Domain.Exceptions.Resources;

namespace Identity.Domain.TenantAggregate.Exceptions;

public class TenantInvitationNotFoundException : ResourceNotFoundException
{
    public TenantInvitationNotFoundException(string token) : base($"Tenant invitation with token {token} was not found.", ErrorCodes.TenantInvitationNotFound)
    {
    }
}