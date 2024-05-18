using Domain.Enums;

namespace Application.Dtos.Submissions.Responses;

public class FormElementDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public FormElementType Type { get; set; }
    
    public double Index { get; set; }
    
    public List<FormElementSettingDto> Settings { get; set; } = new();
    
    public List<OptionFormElementSettingDto> Options { get; set; } = new();
}

public class FormElementSettingDto
{
    public int Id { get; set; }
    
    public FormElementSettingType Type { get; set; }
    
    public string Value { get; set; } = null!;
}

public class OptionFormElementSettingDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
}