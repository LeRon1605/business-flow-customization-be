using BuildingBlocks.Application.FileUploader;
using BuildingBlocks.Shared.Extensions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Cloudinary;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        // "Cloudinary": {
        //     "CloudName": "",
        //     "ApiKey": "",
        //     "ApiSecret": ""
        // }
        
        services.AddScoped<CloudinaryDotNet.Cloudinary>(provider =>
        {
            var cloudinarySection = configuration.GetSection("Cloudinary");
            var account = new Account()
            {
                Cloud = cloudinarySection.GetRequiredValue("CloudName"),
                ApiKey = cloudinarySection.GetRequiredValue("ApiKey"),
                ApiSecret = cloudinarySection.GetRequiredValue("ApiSecret")
            };
            
            return new CloudinaryDotNet.Cloudinary(account);
        });

        services.AddScoped<IFileUploader, CloudinaryFileUploader>();
        
        return services;
    }
}