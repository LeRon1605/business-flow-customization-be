using BuildingBlocks.Application.MailSender;
using BuildingBlocks.Application.MailSender.Interfaces;
using BuildingBlocks.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Mail;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionSetting<EmailSetting>(configuration, nameof(EmailMessage));
        services.AddScoped<IEmailTemplateGenerator, EmailTemplateGenerator>();
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}