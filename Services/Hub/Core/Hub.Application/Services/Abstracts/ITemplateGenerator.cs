using BuildingBlocks.Kernel.Services;
using Domain.Enums;

namespace Hub.Application.Services.Abstracts;

public interface ITemplateGenerator : IScopedService
{
    string GenerateEmailBody(string templateName, Dictionary<string, string> model);
    
    string GenerateNotificationTitle(NotificationType type, Dictionary<string, string> model);
    
    string GenerateNotificationContent(NotificationType type, Dictionary<string, string> model);
}