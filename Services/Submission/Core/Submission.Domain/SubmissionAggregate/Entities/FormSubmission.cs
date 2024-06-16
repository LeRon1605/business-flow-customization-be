using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainEvents;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class FormSubmission : AuditableTenantAggregateRoot
{
    public string Name { get; private set; }
    
    public int FormVersionId { get; private set; }
    
    public int? ExecutionId { get; private set; }
    
    public int BusinessFlowVersionId { get; private set; }
    
    public string? TrackingEmail { get; private set; }
    
    public string? TrackingToken { get; private set; }
    
    public virtual FormVersion FormVersion { get; private set; } = null!;
    
    public virtual List<SubmissionNumberValue> NumberFields { get; private set; } = new();

    public virtual List<SubmissionTextValue> TextFields { get; private set; } = new();

    public virtual List<SubmissionOptionField> OptionFields { get; private set; } = new();

    public virtual List<SubmissionDateValue> DateFields { get; private set; } = new();

    public virtual List<SubmissionAttachmentField> AttachmentFields { get; private set; } = new();

    public FormSubmission(string name, int? executionId, int formVersionId, int businessFlowVersionId, string? trackingEmail)
    {
        Name = name;
        FormVersionId = formVersionId;
        BusinessFlowVersionId = businessFlowVersionId;
        ExecutionId = executionId;
        TrackingEmail = trackingEmail;
        
        if (!executionId.HasValue)
            AddDomainEvent(new FormSubmissionCreatedDomainEvent(this));

        if (!string.IsNullOrWhiteSpace(trackingEmail))
        {
            TrackingToken = Guid.NewGuid().ToString();
            AddDomainEvent(new FormSubmissionTrackingEmailCreatedDomainEvent(this));   
        }
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
    
    public void UpdateField(ISubmissionField field)
    {
        switch (field)
        {
            case SubmissionNumberValue numberField:
                UpdateField(NumberFields, numberField);
                break;
            
            case SubmissionTextValue textField:
                UpdateField(TextFields, textField);
                break;
            
            case SubmissionOptionField optionField:
                UpdateField(OptionFields, optionField);
                break;
            
            case SubmissionDateValue dateField:
                UpdateField(DateFields, dateField);
                break;
            
            case SubmissionAttachmentField attachmentField:
                UpdateField(AttachmentFields, attachmentField);
                break;
        }
    }
    
    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void UpdateField<T, TField>(List<T> fields, TField field) where T : ISubmissionField<TField> where TField : ISubmissionField
    {
        var existingField = fields.FirstOrDefault(f => f.ElementId == field.ElementId);
        if (existingField == null)
        {
            AddField(field);
            return;   
        }
        
        existingField.UpdateValue(field);
    }

    private FormSubmission()
    {
        
    }
}