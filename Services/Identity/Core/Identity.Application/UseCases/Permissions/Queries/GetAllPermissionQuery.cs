using Application.Dtos;
using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Permissions.Dtos;

namespace Identity.Application.UseCases.Permissions.Queries;

public class GetAllPermissionQuery : IQuery<IEnumerable<PermissionDto>>
{
    public string Name { get; set; }
    
    public GetAllPermissionQuery(string name)
    {
        Name = name;
    }
}