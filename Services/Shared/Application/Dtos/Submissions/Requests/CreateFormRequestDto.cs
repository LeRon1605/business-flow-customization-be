using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Submissions.Requests;

public class CreateFormRequestDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string CoverImageUrl { get; set; } = null!;
    
    public List<FormElementRequestDto> Elements { get; set; } = new();
}