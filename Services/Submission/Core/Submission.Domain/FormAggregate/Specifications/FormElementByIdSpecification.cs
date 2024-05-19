using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class FormElementByIdSpecification : Specification<FormElement, int>
{
    private readonly int _elementId;
    
    public FormElementByIdSpecification(int elementId)
    {
        _elementId = elementId;
    }
    
    public override Expression<Func<FormElement, bool>> ToExpression()
    {
        return element => element.Id == _elementId;
    }
}