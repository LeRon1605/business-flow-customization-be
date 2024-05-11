namespace Submission.Domain.SubmissionAggregate.Models;

public class SubmissionOptionFieldModel
{
    public int ElementId { get; set; }

    public int[] OptionIds { get; set; } = null!;
}