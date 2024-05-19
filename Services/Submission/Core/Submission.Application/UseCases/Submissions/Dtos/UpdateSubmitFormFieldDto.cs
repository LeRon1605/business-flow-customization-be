using System.ComponentModel.DataAnnotations;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class UpdateSubmitFormFieldDto
{
    [Required]
    public SubmissionFieldDto Field { get; set; } = null!;
}