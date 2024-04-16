using BuildingBlocks.Application.Cqrs;
using Identity.Application.UseCases.Auth.Dtos;

namespace Identity.Application.UseCases.Auth.Commands;

public class RefreshTokenCommand : ICommand<AuthCredentialDto>
{
    public string RefreshToken { get; set; }
    public string AccessToken { get; set; }

    public RefreshTokenCommand(string refreshToken, string accessToken)
    {
        RefreshToken = refreshToken;
        AccessToken = accessToken;
    }
}