using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Auth.Dtos;

namespace Identity.Application.UseCases.Auth.Commands;

public class SignInCommand : ICommand<AuthCredentialDto>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }

    public SignInCommand(string userNameOrEmail, string password)
    {
        UserNameOrEmail = userNameOrEmail;
        Password = password;
    }
}