﻿using BuildingBlocks.Domain.Models;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

public class SubmissionExecution : AuditableTenantAggregateRoot
{
    public int BusinessFlowBlockId { get; private set; }
    
    public int SubmissionId { get; private set; }
    
    public int? OutComeId { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public virtual BusinessFlowOutCome? OutCome { get; private set; }
    
    public SubmissionExecution(int businessFlowBlockId, int submissionId)
    {
        BusinessFlowBlockId = businessFlowBlockId;
        SubmissionId = submissionId;
    }
}