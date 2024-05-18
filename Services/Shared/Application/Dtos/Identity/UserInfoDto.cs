namespace Application.Dtos.Identity;

public class UserInfoDto : BasicUserInfoDto
{
    public int TenantId { get; set; }
    public bool IsTenantOwner { get; set; }
    public IEnumerable<string> Roles { get; set; } = null!;
    public IEnumerable<string> Permissions { get; set; } = null!;
    public IEnumerable<TenantDto> Tenants { get; set; } = null!;
    public IEnumerable<BasicUserInfoDto> TenantUsers { get; set; } = null!;
}

public class BasicUserInfoDto
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AvatarUrl { get; set; } = null!;
}