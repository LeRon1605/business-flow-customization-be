using BuildingBlocks.Application.Data;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Presentation.ExceptionHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation;

public static class ApplicationBuilderExtensions
{
    public static void UseExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var exception = exceptionHandlerPathFeature?.Error;

                if (exception != null)
                {
                    using var scope = app.ApplicationServices.CreateScope();

                    var exceptionHandler = scope.ServiceProvider.GetRequiredService<IExceptionHandler>();
                    await exceptionHandler.HandleAsync(context, exception);
                }
            });
        });
    }
    
    public static async Task ApplyMigrationAsync<T>(this WebApplication app) where T : DbContext
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<T>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            app.Logger.LogInformation("Migrating pending migration...");   

            await context.Database.MigrateAsync();

            app.Logger.LogInformation("Migrated successfully!");
        }
        else
        {
            app.Logger.LogInformation("No pending migration!");
        }
        
        await app.SeedDataAsync();
    }

    public static WebApplication RegisterCommonPipelines(this WebApplication app)
    {
        app.UseExceptionHandling();

        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();   
        }

        app.UseCors("BusinessFlow");
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
    
    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<IDataSeeder>>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}