using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Submissions.Requests;

public class FormElementRequestDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
    
    [Required]
    public FormElementType Type { get; set; }
    
    [Required]
    public double Index { get; set; }
    
    public List<FormElementSettingDto> Settings { get; set; } = new();
    
    public List<OptionFormElementSettingDto> Options { get; set; } = new();
}

public class FormElementSettingDto
{
    [Required]
    public FormElementSettingType Type { get; set; }
    
    [Required]
    public string Value { get; set; } = null!;
}

public class OptionFormElementSettingDto
{
    [Required]
    public string Name { get; set; } = null!;
}