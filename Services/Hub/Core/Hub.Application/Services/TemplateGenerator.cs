using Domain.Enums;
using Hub.Application.Services.Abstracts;
using Microsoft.Extensions.Hosting;

namespace Hub.Application.Services;

public class TemplateGenerator : ITemplateGenerator
{
    private readonly IHostEnvironment _env;
    
    public TemplateGenerator(IHostEnvironment env)
    {
        _env = env;
    }
    
    public string GenerateEmailBody(string templateName, Dictionary<string, string> model)
    {
        return GenerateTemplate("EmailTemplates", templateName, model);
    }

    public string GenerateNotificationTitle(NotificationType type, Dictionary<string, string> model)
    {
        return GenerateTemplate("NotificationTemplates", $"{type.ToString()}.Title", model);
    }

    public string GenerateNotificationContent(NotificationType type, Dictionary<string, string> model)
    {
        return GenerateTemplate("NotificationTemplates", $"{type.ToString()}.Content", model);
    }

    private string GenerateTemplate(string rootPath, string templateName, Dictionary<string, string> model)
    {
        var template = GetTemplate(rootPath, templateName);
        foreach (var key in model.Keys)
        {
            var name = key;
            var value = model[key];

            template = template.Replace("{{" + name + "}}", value);
        }

        return template;
    }
    
    private string GetTemplate(string rootPath, string templateName)
    {
        var pathToFile = GetTemplatePath(rootPath, templateName);
        
        using var reader = System.IO.File.OpenText(pathToFile);
        return reader.ReadToEnd();
    }

    private string GetTemplatePath(string rootPath, string templateName)
    {
        return $"{_env.ContentRootPath}{Path.DirectorySeparatorChar}wwwroot{Path.DirectorySeparatorChar}{rootPath}{Path.DirectorySeparatorChar}{templateName}.html";
    }
}