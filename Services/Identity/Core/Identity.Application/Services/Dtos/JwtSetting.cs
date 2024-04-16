namespace Identity.Application.Services.Dtos;

public class JwtSetting
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
    public int ExpiresInMinute { get; set; }
    public int RefreshTokenExpiresInDay { get; set; }
}