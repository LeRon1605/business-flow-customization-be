namespace Identity.Application.UseCases.Auth.Dtos;

public class AuthCredentialDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}