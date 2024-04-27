using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Tenants.Commands;

public class InitAccountTenantInvitationCommand : ICommand
{
    public string FullName { get; set; }
    public string Token { get; set; }
    public string Password { get; set; }
    
    public InitAccountTenantInvitationCommand(string fullName, string token, string password)
    {
        FullName = fullName;
        Token = token;
        Password = password;
    }
}