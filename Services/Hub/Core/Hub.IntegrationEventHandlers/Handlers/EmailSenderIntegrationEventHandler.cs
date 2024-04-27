using BuildingBlocks.Application.MailSender;
using BuildingBlocks.EventBus.Abstracts;
using Hub.Application.MailSender;
using IntegrationEvents.Hub;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Hub.IntegrationEventHandlers.Handlers;

public class EmailSenderIntegrationEventHandler : IIntegrationEventHandler<EmailSenderIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateGenerator _emailTemplateGenerator;
    private readonly ILogger<EmailSenderIntegrationEventHandler> _logger;

    public EmailSenderIntegrationEventHandler(IEmailSender emailSender
        , IEmailTemplateGenerator emailTemplateGenerator
        , ILogger<EmailSenderIntegrationEventHandler> logger)
    {
        _emailSender = emailSender;
        _emailTemplateGenerator = emailTemplateGenerator;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<EmailSenderIntegrationEvent> context)
    {
        _logger.LogInformation("Received email sender integration event: {Event}", context.Message.Subject);
        
        var emailMessage = new EmailMessage
        {
            ToAddress = context.Message.ToAddress,
            Subject = context.Message.Subject,
            Body = _emailTemplateGenerator.GenerateEmailBody(context.Message.TemplateName, context.Message.Data)
        };

        await _emailSender.SendAsync(emailMessage);
    }
}