namespace Hub.Application.Dtos;

public class NotificationTemplateDto
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    
    public Dictionary<string, string> MetaData { get; set; } = new();
}