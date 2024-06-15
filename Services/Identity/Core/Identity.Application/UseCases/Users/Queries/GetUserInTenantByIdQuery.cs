using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Users.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserInTenantByIdQuery : IQuery<UserDto>
{
    public string Id { get; set; }
    
    public GetUserInTenantByIdQuery(string id)
    {
        Id = id;
    }
}