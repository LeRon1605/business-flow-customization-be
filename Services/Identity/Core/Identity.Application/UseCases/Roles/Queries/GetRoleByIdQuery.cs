using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Roles.Dtos;

namespace Identity.Application.UseCases.Roles.Queries;

public class GetRoleByIdQuery : IQuery<RoleDto>
{
    public string Id { get; set; }

    public GetRoleByIdQuery(string id)
    {
        Id = id;
    }
}