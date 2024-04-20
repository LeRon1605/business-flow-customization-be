namespace Domain.Permissions;

public static class AppPermission
{
    public const string Default = "Permissions";
    
    public static class Roles
    {
        public const string Group = $"{Default}.Roles";
        
        public const string Management = $"{Group}.Management";

        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.Edit";
        public const string Create = $"{Group}.Create";
        public const string Delete = $"{Group}.Delete";

        public const string ViewPermission = $"{Group}.ViewPermissions";
        public const string UpdatePermission = $"{Group}.UpdatePermissions";
    }
    
    public static class PermissionManagement
    {
        public const string Group = $"{Default}.PermissionManagement";

        public const string View = $"{Group}.View";
    }

    public static class Users
    {
        public const string Group = $"{Default}.Users";

        public const string Management = $"{Group}.Management";
        public const string ViewProfile = $"{Group}.ViewProfile";
        public const string ViewPermissions = $"{Group}.ViewPermissions";
        public const string View = $"{Group}.View";
        public const string ViewRoles = $"{Group}.ViewRoles";
        public const string EditRoles = $"{Group}.EditRoles";
    }
    
    public static class Tenants
    {
        public const string Group = $"{Default}.Tenants";

        public const string Management = $"{Group}.Management";
        public const string View = $"{Group}.View";
        public const string Edit = $"{Group}.ViewRoles";
    }
}