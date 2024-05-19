using System.ComponentModel.DataAnnotations;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class UpdateSubmitFormNameDto
{
    [Required]
    public string Name { get; set; } = null!;
}