using BuildingBlocks.Application.Data;
using Domain;
using Domain.Identities;
using Domain.Roles;
using Identity.Application.Services.Interfaces;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.TenantAggregate;
using Identity.Domain.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Seeders;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly IIdentityService _identityService;
    private readonly ITenantService _tenantService;
    private readonly ITenantRepository _tenantRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<IdentityDataSeeder> _logger;

    public IdentityDataSeeder(IIdentityService identityService
        , ITenantService tenantService
        , ITenantRepository tenantRepository
        , UserManager<ApplicationUser> userManager
        , RoleManager<ApplicationRole> roleManager
        , ILogger<IdentityDataSeeder> logger)
    {
        _identityService = identityService;
        _tenantService = tenantService;
        _tenantRepository = tenantRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Begin seeding identity data...");

            await SeedDefaultRoleAsync();
            await SeedDefaultTenantAsync();
            await SeedDefaultAdminAccountAsync();
            
            _logger.LogInformation("Seed identity data successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding identity data failed: {Message}!", ex.Message);
        }
    }

    private async Task SeedDefaultAdminAccountAsync()
    {
        if (await _userManager.Users.AnyAsync())
        {
            return;
        }
        
        var user = await _identityService.CreateUserAsync(
            "ServeSync",
            "admin",
            "https://res.cloudinary.com/dboijruhe/image/upload/v1700832514/Assets/ddxrzqclm5ysut4o9qie.png",
            "admin@gmail.com",
            "admin123");

        if (user.IsSuccess)
        {
            await _tenantService.AddUserToTenantAsync(user.Data!.Id
                , true
                , AppTenant.Default);
            await _identityService.GrantToRoleAsync(user.Data.Id, AppRole.Admin, AppTenant.Default);    
        }
    }

    private async Task SeedDefaultRoleAsync()
    {
        if (await _roleManager.Roles.AnyAsync())
        {
            return;
        }

        var defaultRoles = ApplicationRole.GetDefaultRoles();
        foreach (var role in defaultRoles)
        {
            await _roleManager.CreateAsync(role);
        }
    }
    
    private async Task SeedDefaultTenantAsync()
    {
        if (await _tenantRepository.AnyAsync())
        {
            return;
        }

        await _tenantService.CreateAsync("Business Flow Executor", AppTenant.DefaultAvatar);
    }
}