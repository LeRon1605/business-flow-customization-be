namespace Submission.Domain.FormAggregate.Models;

public class FormModel
{
    public Guid? BusinessFlowBlockId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string CoverImageUrl { get; set; } = null!;
    
    public List<FormElementModel> Elements { get; set; } = new();
}