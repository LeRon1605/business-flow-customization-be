using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Roles.Dtos;

namespace Identity.Application.UseCases.Roles.Commands;

public class CreateRoleCommand : ICommand<RoleDto>
{
    public string Name { get; set; }

    public CreateRoleCommand(string name)
    {
        Name = name;
    }
}