using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

public class SubmissionExecutionByBlockSpecification : Specification<SubmissionExecution>
{
    private readonly int _submissionId;
    private readonly Guid _blockId;
    
    public SubmissionExecutionByBlockSpecification(int submissionId, Guid blockId)
    {
        _submissionId = submissionId;
        _blockId = blockId;
    }
    
    public override Expression<Func<SubmissionExecution, bool>> ToExpression()
    {
        return submissionExecution => submissionExecution.SubmissionId == _submissionId 
                                      && submissionExecution.BusinessFlowBlockId == _blockId;
    }
}