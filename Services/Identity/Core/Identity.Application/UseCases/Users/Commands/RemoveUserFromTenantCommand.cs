using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Users.Commands;

public class RemoveUserFromTenantCommand : ICommand
{
    public string UserId { get; set; }
    
    public RemoveUserFromTenantCommand(string userId)
    {
        UserId = userId;
    }
}