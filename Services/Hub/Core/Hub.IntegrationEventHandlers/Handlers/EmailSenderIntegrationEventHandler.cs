using BuildingBlocks.Application.Identity;
using BuildingBlocks.Application.MailSender;
using BuildingBlocks.EventBus;
using Hub.Application.MailSender;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace Hub.IntegrationEventHandlers.Handlers;

public class EmailSenderIntegrationEventHandler : IntegrationEventHandler<EmailSenderIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateGenerator _emailTemplateGenerator;

    public EmailSenderIntegrationEventHandler(IEmailSender emailSender
        , IEmailTemplateGenerator emailTemplateGenerator
        , IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _emailSender = emailSender;
        _emailTemplateGenerator = emailTemplateGenerator;
    }

    public override async Task HandleAsync(EmailSenderIntegrationEvent @event)
    {
        Logger.LogInformation("Received email sender integration event: {Event}", @event.Subject);
        
        var emailMessage = new EmailMessage
        {
            ToAddress = @event.ToAddress,
            Subject = @event.Subject,
            Body = _emailTemplateGenerator.GenerateEmailBody(@event.TemplateName, @event.Data)
        };

        await _emailSender.SendAsync(emailMessage);
    }
}