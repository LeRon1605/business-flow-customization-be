using System.ComponentModel.DataAnnotations;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmitFormDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public int FormVersionId { get; set; }
    
    [Required]
    public List<SubmissionFieldDto> Fields { get; set; } = null!;
}

public class SubmissionFieldDto
{
    [Required]
    public int ElementId { get; set; }
    
    public string? Value { get; set; }
}