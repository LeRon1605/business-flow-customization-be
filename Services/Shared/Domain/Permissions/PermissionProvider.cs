namespace Domain.Permissions;

public class PermissionProvider
{
    public static Dictionary<string, string> Provider => new()
    {
        { AppPermission.Roles.Management, "Quản lý roles." },
        { AppPermission.Roles.Create, "Tạo role." },
        { AppPermission.Roles.View, "Xem role." },
        { AppPermission.Roles.Edit, "Chỉnh sửa roles." },
        { AppPermission.Roles.Delete, "Xóa roles." },
        { AppPermission.Roles.ViewPermission, "Xem quyền của role." },
        { AppPermission.Roles.UpdatePermission, "Cập nhật quyền cho role." },

        { AppPermission.PermissionManagement.View, "Xem danh sách quyền." },
        
        { AppPermission.Users.Management, "Quản lý người dùng." },
        { AppPermission.Users.ViewPermissions, "Xem quyền của người dùng." },
        { AppPermission.Users.ViewProfile, "Xem thông tin cá nhân." },
        { AppPermission.Users.View, "Xem danh sách người dùng." },
        { AppPermission.Users.ViewRoles, "Xem vai trò người dùng." },
        { AppPermission.Users.EditRoles, "Cập nhật vai trò người dùng." },
        
        { AppPermission.Tenants.Management, "Quản lý doanh nghiệp" },
        { AppPermission.Tenants.View, "Xem thông tin doanh nghiệp" },
        { AppPermission.Tenants.Edit, "Cập nhật thông tin doanh nghiệp" },
        { AppPermission.Tenants.InviteMember, "Mời thành viên vào doanh nghiệp" }
    };
}