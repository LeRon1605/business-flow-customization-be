namespace BuildingBlocks.Application.MailSender;

public class EmailMessage
{
    public string ToAddress { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}