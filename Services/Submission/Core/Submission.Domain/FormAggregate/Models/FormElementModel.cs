using Domain.Enums;

namespace Submission.Domain.FormAggregate.Models;

public class FormElementModel
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    public FormElementType Type { get; set; }
    
    public double Index { get; set; }
    
    public List<FormElementSettingModel> Settings { get; set; } = new();
    
    public List<OptionFormElementSettingModel> Options { get; set; } = new();
}

public class FormElementSettingModel
{
    public FormElementSettingType Type { get; set; }
    
    public string Value { get; set; } = null!;
}

public class OptionFormElementSettingModel
{
    public string Name { get; set; } = null!;
}