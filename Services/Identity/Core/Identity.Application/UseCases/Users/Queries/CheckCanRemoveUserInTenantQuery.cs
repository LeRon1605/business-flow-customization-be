using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Users.Queries;

public class CheckCanRemoveUserInTenantQuery : IQuery<bool>
{
    public string Id { get; set; }
    public CheckCanRemoveUserInTenantQuery(string id)
    {
        Id = id;
    }
}