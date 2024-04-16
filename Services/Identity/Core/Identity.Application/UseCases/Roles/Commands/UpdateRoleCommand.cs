using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Roles.Dtos;

namespace Identity.Application.UseCases.Roles.Commands;

public class UpdateRoleCommand : ICommand<RoleDto>
{
    public string Id { get; set; }
    public string Name { get; set; }

    public UpdateRoleCommand(string id, string name)
    {
        Id = id;
        Name = name;
    }
}