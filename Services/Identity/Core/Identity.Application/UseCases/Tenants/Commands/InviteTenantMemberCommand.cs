using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Tenants.Commands;

public class InviteTenantMemberCommand : ICommand
{
    public string Email { get; set; }
    public string RoleId { get; set; }
    
    public InviteTenantMemberCommand(string email, string roleId)
    {
        Email = email;
        RoleId = roleId;
    }
}