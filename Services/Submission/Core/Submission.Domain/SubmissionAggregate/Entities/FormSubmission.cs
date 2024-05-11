using BuildingBlocks.Domain.Models;
using Submission.Domain.SubmissionAggregate.Abstracts;

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
    
    public void AddField(ISubmissionField field)
    {
        switch (field)
        {
            case SubmissionNumberValue numberField:
                NumberFields.Add(numberField);
                break;
            
            case SubmissionTextValue textField:
                TextFields.Add(textField);
                break;
            
            case SubmissionOptionField optionField:
                OptionFields.Add(optionField);
                break;
            
            case SubmissionDateValue dateField:
                DateFields.Add(dateField);
                break;
            
            case SubmissionAttachmentField attachmentField:
                AttachmentFields.Add(attachmentField);
                break;
        }
    }

    private FormSubmission()
    {
        
    }
}