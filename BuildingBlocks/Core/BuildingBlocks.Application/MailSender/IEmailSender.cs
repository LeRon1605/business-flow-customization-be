using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.Application.MailSender;

public interface IEmailSender : IScopedService
{
    Task SendAsync(EmailMessage emailMessage);
    
    void Push(EmailMessage emailMessage);
}