﻿using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionAttachmentField : AuditableEntity, ISubmissionField<SubmissionAttachmentField>
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
    
    public virtual FormElement Element { get; private set; } = null!;
    
    public virtual FormSubmission Submission { get; private set; } = null!;

    public virtual List<SubmissionAttachmentValue> Values { get; private set; } = new();
    
    public SubmissionAttachmentField(int elementId)
    {
        ElementId = elementId;
    }
    
    public SubmissionAttachmentField(int elementId, SubmissionAttachmentValueModel[] attachments) : this(elementId)
    {
        AddAttachments(attachments);
    }
    
    public void AddAttachments(SubmissionAttachmentValueModel[] attachments)
    {
        foreach (var attachment in attachments)
        {
            Values.Add(new SubmissionAttachmentValue(attachment.Name, attachment.FileUrl));
        }
    }
    
    public void UpdateValue(SubmissionAttachmentField field)
    {
        Values = field.Values;
    }
    
    private SubmissionAttachmentField()
    {
        
    }
}