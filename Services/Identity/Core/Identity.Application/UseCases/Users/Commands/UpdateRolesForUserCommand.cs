using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Users.Commands;

public class UpdateRolesForUserCommand : ICommand
{
    public string UserId { get; set; }
    public int TenantId { get; set; }
    public IEnumerable<string> RoleIds { get; set; }
    
    public UpdateRolesForUserCommand(string userId, int tenantId, IEnumerable<string> roleIds)
    {
        UserId = userId;
        TenantId = tenantId;
        RoleIds = roleIds;
    }
}