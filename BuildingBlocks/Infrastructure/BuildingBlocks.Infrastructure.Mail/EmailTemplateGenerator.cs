using BuildingBlocks.Application.MailSender.Interfaces;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.Application.MailSender;

public class EmailTemplateGenerator : IEmailTemplateGenerator
{
    private readonly IHostEnvironment _env;
    
    public EmailTemplateGenerator(IHostEnvironment env)
    {
        _env = env;
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