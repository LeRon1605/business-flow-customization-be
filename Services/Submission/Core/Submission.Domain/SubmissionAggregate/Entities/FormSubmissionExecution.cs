using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class FormSubmissionExecution : AggregateRoot
{
    public string Name { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public int FormSubmissionId { get; private set; }
    
    public virtual FormSubmission FormSubmission { get; private set; }
    
    public FormSubmissionExecution(int id, string name, DateTime createdAt, int formSubmissionId)
    {
        Id = id;
        Name = name;
        FormSubmissionId = formSubmissionId;
        CreatedAt = DateTime.UtcNow;
    }

    private FormSubmissionExecution()
    {
        
    }
}