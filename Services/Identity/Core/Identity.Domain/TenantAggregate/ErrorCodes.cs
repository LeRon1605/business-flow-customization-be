namespace Identity.Domain.TenantAggregate;

public static class ErrorCodes
{
    public const string TenantNotFound = "Tenant:000001";
    
    public const string TenantInvitationAlreadyExisted = "TenantInvitation:000001";
    public const string UserAlreadyInTenant = "TenantInvitation:000002";
    public const string TenantInvitationNotFound = "TenantInvitation:000003";
}