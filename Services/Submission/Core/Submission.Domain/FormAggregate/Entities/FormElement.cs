using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Enums;

namespace Submission.Domain.FormAggregate.Entities;

public class FormElement : TenantEntity
{
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public FormElementType Type { get; private set; }
    
    public double Index { get; private set; }
    
    public int FormVersionId { get; private set; }

    public virtual FormVersion FormVersion { get; private set; } = null!;
    
    public virtual List<FormElementSetting> Settings { get; private set; } = new();
    
    public virtual List<OptionFormElementSetting> Options { get; private set; } = new();
    
    public FormElement(string name, string description, FormElementType type, double index)
    {
        Name = name;
        Description = description;
        Type = type;
        Index = index;
    }
    
    private FormElement()
    {
    }
}