using BuildingBlocks.Application.Data;
using BuildingBlocks.Application.Identity;
using Domain.Identities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Seeders;

public abstract class DataSeeder : IDataSeeder
{
    public abstract int Id { get; }

    protected ICurrentUser CurrentUser { get; set; }

    public DataSeeder(IServiceProvider serviceProvider)
    {
        CurrentUser = serviceProvider.GetRequiredService<ICurrentUser>();

        CurrentUser.Id = AppUser.Id;
        CurrentUser.TenantId = AppTenant.Default;
    }
    
    public abstract Task SeedAsync();
}