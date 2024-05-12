using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class FormVersion : AuditableTenantAggregateRoot
{
    public int FormId { get; set; }
    
    public virtual Form Form { get; set; } = null!;
    
    public virtual List<FormElement> Elements { get; set; } = new();
    
    public FormVersion(List<FormElementModel> elements)
    {
        AddElements(elements);
    }
    
    public void AddElements(List<FormElementModel> elements)
    {
        foreach (var element in elements)
        {
            Elements.Add(new FormElement(element));
        }
    }
    
    private FormVersion()
    {
    }
}