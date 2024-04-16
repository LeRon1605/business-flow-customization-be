using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Users.Dtos;

namespace Identity.Application.UseCases.Users.Queries;

public class GetUserByIdQuery : IQuery<UserDetailDto>
{
    public string Id { get; set; }
    
    public GetUserByIdQuery(string id)
    {
        Id = id;
    }
    
}