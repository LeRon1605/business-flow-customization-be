namespace Domain.Permissions;

public static class StaffPermissionProvider
{
    public static List<string> Provider = new()
    {
        AppPermission.Users.ViewProfile,
        AppPermission.Tenants.View,
        
        AppPermission.Space.Management,
        AppPermission.Space.View
    };
}