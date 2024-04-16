using BuildingBlocks.Application.MailSender;
using BuildingBlocks.EventBus.Abstracts;
using Hub.Application.MailSender;
using IntegrationEvents.Hub;
using MassTransit;

namespace Hub.IntegrationEventHandlers.Handlers;

public class EmailSenderIntegrationEventHandler : IIntegrationEventHandler<EmailSenderIntegrationEvent>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailTemplateGenerator _emailTemplateGenerator;

    public EmailSenderIntegrationEventHandler(IEmailSender emailSender, IEmailTemplateGenerator emailTemplateGenerator)
    {
        _emailSender = emailSender;
        _emailTemplateGenerator = emailTemplateGenerator;
    }
    
    public async Task Consume(ConsumeContext<EmailSenderIntegrationEvent> context)
    {
        var emailMessage = new EmailMessage
        {
            ToAddress = context.Message.ToAddress,
            Subject = context.Message.Subject,
            Body = _emailTemplateGenerator.GenerateEmailBody(context.Message.TemplateName, context.Message.Data)
        };

        await _emailSender.SendAsync(emailMessage);
    }
}