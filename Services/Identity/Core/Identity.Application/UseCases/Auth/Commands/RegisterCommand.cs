using BuildingBlocks.Application.Cqrs;

namespace Identity.Application.UseCases.Auth.Commands;

public class RegisterCommand : ICommand
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string TenantName { get; set; }
    public string Password { get; set; }
    
    public RegisterCommand(string email, string fullName, string tenantName, string password)
    {
        Email = email;
        FullName = fullName;
        TenantName = tenantName;
        Password = password;
    }
}