using Microsoft.Extensions.Hosting;

namespace Hub.Application.MailSender;

public class EmailTemplateGenerator : IEmailTemplateGenerator
{
    private readonly IHostEnvironment _env;
    
    public EmailTemplateGenerator(IHostEnvironment env)
    {
        _env = env;
    }
    
    public string GenerateEmailBody(string templateName, Dictionary<string, string> model)
    {
        var template = GetTemplate(templateName);
        foreach (var key in model.Keys)
        {
            var name = key;
            var value = model[key];

            template = template.Replace("{{" + name + "}}", value);
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