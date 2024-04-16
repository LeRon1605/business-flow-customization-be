namespace Identity.Application.Services.Dtos;

public class ForgetPasswordSetting
{
    public int ExpiresInMinute { get; set; }
    public string[] AllowedClients { get; set; } = null!;
}