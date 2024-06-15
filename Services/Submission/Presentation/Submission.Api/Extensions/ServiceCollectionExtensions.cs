using BuildingBlocks.Shared.Extensions;
using Submission.Application.Services.Dtos;

namespace Submission.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddSettings(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptionSetting<PublicFormSetting>(builder.Configuration, nameof(PublicFormSetting));
        
        return builder;
    }
}