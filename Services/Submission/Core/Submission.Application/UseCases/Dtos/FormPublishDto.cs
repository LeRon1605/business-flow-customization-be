namespace Submission.Application.UseCases.Dtos;

public class FormPublishDto
{
    public bool IsPublished { get; set; }
    public string PublicToken { get; set; } = null!;
    
    public FormPublishDto(bool isPublished, string publicToken)
    {
        IsPublished = isPublished;
        PublicToken = publicToken;
    }
}