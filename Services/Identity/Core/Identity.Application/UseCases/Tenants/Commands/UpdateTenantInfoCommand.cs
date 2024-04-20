using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Tenants.Commands;

public class UpdateTenantInfoCommand : ICommand
{
    public string Name { get; set; }
    public string AvatarUrl { get; set; }

    public UpdateTenantInfoCommand(string name, string avatarUrl)
    {
        Name = name;
        AvatarUrl = avatarUrl;
    }
}