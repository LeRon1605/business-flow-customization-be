using BuildingBlocks.Application.Data;
using Domain.Permissions;
using Domain.Roles;
using Identity.Domain.PermissionAggregate;
using Identity.Domain.PermissionAggregate.Entities;
using Identity.Domain.PermissionAggregate.Specifications;
using Identity.Domain.RoleAggregate;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Seeders;

public class PermissionDataSeeder : IDataSeeder
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PermissionDataSeeder> _logger;

    private Dictionary<string, IEnumerable<string>> PermissionsByRole => new Dictionary<string, IEnumerable<string>>()
    {
        { AppRole.Admin, AdminPermissionProvider.Provider },
        { AppRole.Manager, ManagerPermissionProvider.Provider },
        { AppRole.Staff, StaffPermissionProvider.Provider }
    };

    public PermissionDataSeeder(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IUnitOfWork unitOfWork,
        ILogger<PermissionDataSeeder> logger)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        await SyncPermissionAsync();
        
        foreach (var role in PermissionsByRole)
        {
            await SeedPermissionForRoleAsync(role.Key);
        }
    }

    private async Task SyncPermissionAsync()
    {
        _logger.LogInformation("Begin syncing permissions...");

        var permissions = await _permissionRepository.FindAllAsync();

        var deletePermissions = permissions.ExceptBy(PermissionProvider.Provider.Select(x => x.Key), x => x.Name);
        var newPermissions = PermissionProvider.Provider.ExceptBy(permissions.Select(x => x.Name), x => x.Key);
        foreach (var permission in deletePermissions)
        {
            _permissionRepository.Delete(permission);
        }

        foreach (var permission in newPermissions)
        {
            await _permissionRepository.InsertAsync(new ApplicationPermission(permission.Key, permission.Value));
        }

        await _unitOfWork.CommitAsync();
    }
    
    private async Task SeedPermissionForRoleAsync(string name)
    {
        _logger.LogInformation("Begin seeding not granted permissions for {Role}...", name);

        var role = await _roleRepository.FindByNameAsync(name);
        if (role == null)
        {
            _logger.LogWarning("{Role} role not found!", name);
            return;
        }

        var permissions =
            await _permissionRepository.FilterAsync(new PermissionByIncludedNameSpecification(PermissionsByRole[name]));
        var notGrantedPermissions =
            permissions.ExceptBy(role.Permissions.Select(x => x.PermissionId), x => x.Id).ToList();
        var deletedPermissions = role.Permissions.ExceptBy(permissions.Select(x => x.Id), x => x.PermissionId).ToList();

        if (!notGrantedPermissions.Any() && !deletedPermissions.Any())
        {
            _logger.LogInformation("{Role} permissions has already in sync!", name);
            return;
        }

        foreach (var permission in notGrantedPermissions)
        {
            role.GrantPermission(permission.Id);
        }

        foreach (var permission in deletedPermissions)
        {
            role.UnGrantPermission(permission.PermissionId);
        }

        _roleRepository.Update(role);
        await _unitOfWork.CommitAsync();
    }
}