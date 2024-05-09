using BuildingBlocks.Domain.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class FormSubmission : AuditableTenantAggregateRoot
{
    public string Name { get; private set; }
    
    public int FormVersionId { get; private set; }
    
    public int BusinessFlowVersionId { get; private set; }
    
    public virtual List<SubmissionNumberValue> NumberFields { get; private set; } = new();

    public virtual List<SubmissionTextValue> TextFields { get; private set; } = new();

    public virtual List<SubmissionOptionField> OptionFields { get; private set; } = new();

    public virtual List<SubmissionDateValue> DateFields { get; private set; } = new();

    public virtual List<SubmissionAttachmentField> AttachmentFields { get; private set; } = new();

    public FormSubmission(string name, int formVersionId, int businessFlowVersionId)
    {
        Name = name;
        FormVersionId = formVersionId;
        BusinessFlowVersionId = businessFlowVersionId;
    }

    private FormSubmission()
    {
        
    }
}