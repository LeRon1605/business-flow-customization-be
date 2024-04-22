namespace Domain.Permissions;

public static class ManagerPermissionProvider
{
    public static List<string> Provider = new()
    {
        AppPermission.Users.ViewPermissions,
        AppPermission.Users.ViewProfile,
        AppPermission.Users.EditRoles,
        AppPermission.Users.Management,
        AppPermission.Users.ViewRoles,
        
        AppPermission.Tenants.Management,
        AppPermission.Tenants.View,
        AppPermission.Tenants.Edit,
        AppPermission.Tenants.InviteMember,
    };
}