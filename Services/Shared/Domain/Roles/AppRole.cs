namespace Domain.Roles;

public static class AppRole
{
    public const string Admin = "Chủ doanh nghiệp";
    public const string Manager = "Quản lý";
    public const string Staff = "Nhân viên";
    
    public static readonly List<string> All = new()
    {
        Admin,
        Manager,
        Staff
    };
}