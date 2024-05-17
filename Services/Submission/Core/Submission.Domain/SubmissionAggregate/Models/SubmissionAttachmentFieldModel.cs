namespace Submission.Domain.SubmissionAggregate.Models;

public class SubmissionAttachmentFieldModel
{
    public int ElementId { get; set; }
    
    public SubmissionAttachmentValueModel[] Attachments { get; set; } = null!;
}

public class SubmissionAttachmentValueModel
{
    public string Name { get; set; } = null!;
    
    public string FileUrl { get; set; } = null!;
}