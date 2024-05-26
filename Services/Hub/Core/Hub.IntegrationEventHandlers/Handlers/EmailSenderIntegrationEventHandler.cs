using BuildingBlocks.Application.Identity;
using BuildingBlocks.Application.MailSender;
using BuildingBlocks.EventBus;
using Hub.Application.Services.Abstracts;
using IntegrationEvents.Hub;
using Microsoft.Extensions.Logging;

namespace Hub.IntegrationEventHandlers.Handlers;

public class EmailSenderIntegrationEventHandler : IntegrationEventHandler<EmailSenderIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly ITemplateGenerator _templateGenerator;

    public EmailSenderIntegrationEventHandler(ICurrentUser currentUser
        , ILogger<IntegrationEventHandler<EmailSenderIntegrationEvent>> logger
        , IEmailSender emailSender
        , ITemplateGenerator templateGenerator) : base(currentUser, logger)
    {
        _emailSender = emailSender;
        _templateGenerator = templateGenerator;
    }

    public override async Task HandleAsync(EmailSenderIntegrationEvent @event)
    {
        Logger.LogInformation("Received email sender integration event: {Event}", @event.Subject);
        
        var emailMessage = new EmailMessage
        {
            ToAddress = @event.ToAddress,
            Subject = @event.Subject,
            Body = _templateGenerator.GenerateEmailBody(@event.TemplateName, @event.Data)
        };

        await _emailSender.SendAsync(emailMessage);
    }
}