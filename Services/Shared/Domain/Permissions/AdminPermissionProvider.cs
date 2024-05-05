namespace Domain.Permissions;

public static class AdminPermissionProvider
{
    public static List<string> Provider = new()
    {
        AppPermission.Roles.Management,
        AppPermission.Roles.Create,
        AppPermission.Roles.View,
        AppPermission.Roles.Edit,
        AppPermission.Roles.Delete,
        AppPermission.Roles.ViewPermission,
        AppPermission.Roles.UpdatePermission,

        AppPermission.PermissionManagement.View,
        
        AppPermission.Users.ViewPermissions,
        AppPermission.Users.ViewProfile,
        AppPermission.Users.EditRoles,
        AppPermission.Users.Management,
        AppPermission.Users.ViewRoles,
        
        AppPermission.Tenants.Management,
        AppPermission.Tenants.View,
        AppPermission.Tenants.Edit,
        AppPermission.Tenants.InviteMember,
        
        AppPermission.Space.Management,
        AppPermission.Space.View,
        AppPermission.Space.Edit,
        AppPermission.Space.Create,
        AppPermission.Space.Delete
    };
}