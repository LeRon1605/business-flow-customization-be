using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Submissions.Requests;

public class FormElementRequestDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public FormElementType Type { get; set; }
    
    [Required]
    public double Index { get; set; }
    
    public List<FormElementSettingRequestDto> Settings { get; set; } = new();
    
    public List<OptionFormElementSettingRequestDto> Options { get; set; } = new();
}

public class FormElementSettingRequestDto
{
    [Required]
    public FormElementSettingType Type { get; set; }
    
    [Required]
    public string Value { get; set; } = null!;
}

public class OptionFormElementSettingRequestDto
{
    [Required]
    public string Name { get; set; } = null!;
}