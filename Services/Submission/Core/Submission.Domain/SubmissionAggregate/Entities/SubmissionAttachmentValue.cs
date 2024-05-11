using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionAttachmentValue : Entity
{
    public int SubmissionFieldId { get; private set; }
    
    public string FileName { get; private set; }
    
    public string FileUrl { get; private set; }
    
    public virtual SubmissionAttachmentField Field { get; private set; } = null!;
    
    public SubmissionAttachmentValue(string fileName, string fileUrl)
    {
        FileName = fileName;
        FileUrl = fileUrl;
    }
    
    private SubmissionAttachmentValue()
    {
        
    }
}