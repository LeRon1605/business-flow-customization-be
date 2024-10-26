﻿using BuildingBlocks.Domain.Models;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;

namespace Submission.Domain.SubmissionAggregate.Entities;

public class SubmissionDateValue : AuditableEntity, ISubmissionField<SubmissionDateValue>
{
    public int SubmissionId { get; set; }
    
    public int ElementId { get; set; }
    
    public virtual FormElement Element { get; private set; } = null!;
    
    public DateTime? Value { get; private set; }

    public virtual FormSubmission Submission { get; private set; } = null!;
    
    public SubmissionDateValue(int elementId)
    {
        ElementId = elementId;
    }
    
    public SubmissionDateValue(int elementId, DateTime value) : this(elementId)
    {
        Value = value;
    }

    private SubmissionDateValue()
    {
        
    }
    
    public void UpdateValue(SubmissionDateValue field)
    {
        Value = field.Value;
    }
}