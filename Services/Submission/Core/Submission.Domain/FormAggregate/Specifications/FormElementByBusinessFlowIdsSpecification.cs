using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class FormElementByBusinessFlowIdsSpecification : Specification<FormElement>
{
    private readonly List<Guid> _businessFlowBlockIds;
    
    public FormElementByBusinessFlowIdsSpecification(List<Guid> businessFlowBlockIds)
    {
        _businessFlowBlockIds = businessFlowBlockIds;
    }
    
    public override Expression<Func<FormElement, bool>> ToExpression()
    {
        return x => x.FormVersion.Form.BusinessFlowBlockId.HasValue 
                    && _businessFlowBlockIds.Contains(x.FormVersion.Form.BusinessFlowBlockId.Value);
    }
}