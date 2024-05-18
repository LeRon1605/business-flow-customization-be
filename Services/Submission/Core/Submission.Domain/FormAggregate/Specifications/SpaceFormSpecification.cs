using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class SpaceFormSpecification : Specification<Form>
{
    private readonly int _spaceId;
    private readonly Guid? _businessFlowBlockId;
    
    public SpaceFormSpecification(int spaceId)
    {
        _spaceId = spaceId;
        _businessFlowBlockId = null;
    }
    
    public SpaceFormSpecification(int spaceId, Guid? businessFlowBlockId)
    {
        _spaceId = spaceId;
        _businessFlowBlockId = businessFlowBlockId;
    }
    
    public override Expression<Func<Form, bool>> ToExpression()
    {
        return x => x.SpaceId == _spaceId && x.BusinessFlowBlockId == _businessFlowBlockId;
    }
}