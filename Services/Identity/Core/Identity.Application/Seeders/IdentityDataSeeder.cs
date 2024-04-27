using Application.Seeders;
using BuildingBlocks.Application.Data;
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

public class IdentityDataSeeder : DataSeeder, IDataSeeder
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
        , ILogger<IdentityDataSeeder> logger
        , IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _identityService = identityService;
        _tenantService = tenantService;
        _tenantRepository = tenantRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public override int Id => 1;

    public override async Task SeedAsync()
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
        
        var user = await _userManager.CreateAsync(ApplicationUser.Default());

        if (user.Succeeded)
        {
            await _tenantService.AddUserToTenantAsync(AppUser.Id
                , true
                , AppTenant.Default);
            await _identityService.GrantToRoleAsync(AppUser.Id, AppRole.Admin, AppTenant.Default);    
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