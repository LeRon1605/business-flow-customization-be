namespace Submission.Domain.FormAggregate.Models;

public class FormModel
{
    public string Name { get; set; } = null!;
    
    public string CoverImageUrl { get; set; } = null!;
    
    public List<FormElementModel> Elements { get; set; } = new();
}