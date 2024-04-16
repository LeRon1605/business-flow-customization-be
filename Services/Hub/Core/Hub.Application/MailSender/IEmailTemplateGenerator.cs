using BuildingBlocks.Kernel.Services;

namespace Hub.Application.MailSender;

public interface IEmailTemplateGenerator : IScopedService
{
    string GenerateEmailBody(string templateName, dynamic model);
}