using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Enums;

namespace Submission.Domain.FormAggregate.Entities;

public class FormElementSetting : Entity
{
    public FormElementSettingType Type { get; private set; }
    
    public string Value { get; private set; }
    
    public int FormElementId { get; private set; }
    
    public virtual FormElement FormElement { get; private set; } = null!;
    
    public FormElementSetting(FormElementSettingType type, string value)
    {
        Type = type;
        Value = value;
    }
    
    private FormElementSetting()
    {
    }
}