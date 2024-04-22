using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Users.Commands;

public class RemoveUserFromTenantCommand : ICommand
{
    public int TenantId { get; set; }
    public string UserId { get; set; }
    
    public RemoveUserFromTenantCommand(int tenantId, string userId)
    {
        TenantId = tenantId;
        UserId = userId;
    }
}