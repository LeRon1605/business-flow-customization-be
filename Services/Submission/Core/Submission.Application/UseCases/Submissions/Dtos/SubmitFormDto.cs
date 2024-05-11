using System.ComponentModel.DataAnnotations;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmitFormDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public int FormVersionId { get; set; }
    
    [Required]
    public List<SubmissionFieldModel> Fields { get; set; } = null!;
}