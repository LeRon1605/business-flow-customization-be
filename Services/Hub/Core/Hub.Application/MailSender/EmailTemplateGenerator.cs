using Microsoft.Extensions.Hosting;

namespace Hub.Application.MailSender;

public class EmailTemplateGenerator : IEmailTemplateGenerator
{
    private readonly IHostEnvironment _env;
    
    public EmailTemplateGenerator(IHostEnvironment env)
    {
        _env = env;
    }
    
    public string GenerateEmailBody(string templateName, dynamic model)
    {
        var template = GetTemplate(templateName);
        var properties = model.GetType().GetProperties();
        foreach (var property in properties)
        {
            var name=property.Name;
            var value = model.GetType().GetProperty(property.Name).GetValue(model, null);

            template = template.Replace($"{{{name}}}", value?.ToString() ?? string.Empty);
        }

        return template;
    }
    
    private string GetTemplate(string templateName)
    {
        var pathToFile = GetTemplatePath(templateName);
        
        using var reader = System.IO.File.OpenText(pathToFile);
        return reader.ReadToEnd();
    }

    private string GetTemplatePath(string templateName)
    {
        return $"{_env.ContentRootPath}{Path.DirectorySeparatorChar}wwwroot{Path.DirectorySeparatorChar}EmailTemplates{Path.DirectorySeparatorChar}{templateName}.html";
    }
}