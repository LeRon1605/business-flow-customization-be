using BuildingBlocks.Domain.Models;
using Domain.Enums;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class FormElementSetting : Entity
{
    public FormElementSettingType Type { get; private set; }
    
    public string Value { get; private set; }
    
    public int FormElementId { get; private set; }
    
    public virtual FormElement FormElement { get; private set; } = null!;
    
    public FormElementSetting(FormElementSettingModel settingModel)
    {
        Type = settingModel.Type;
        Value = settingModel.Value;
    }
    
    private FormElementSetting()
    {
    }
}