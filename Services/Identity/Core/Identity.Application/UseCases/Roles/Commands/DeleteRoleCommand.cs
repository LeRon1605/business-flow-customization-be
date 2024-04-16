using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Roles.Commands;

public class DeleteRoleCommand : ICommand
{
    public string Id { get; set; }

    public DeleteRoleCommand(string id)
    {
        Id = id;
    }
}