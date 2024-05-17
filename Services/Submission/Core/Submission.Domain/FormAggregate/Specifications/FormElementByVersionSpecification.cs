using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Domain.FormAggregate.Specifications;

public class FormElementByVersionSpecification : Specification<FormElement>
{
    private readonly int _formVersionId;
    
    public FormElementByVersionSpecification(int formVersionId)
    {
        _formVersionId = formVersionId;
        AddInclude(x => x.Settings);
    }
    
    public override Expression<Func<FormElement, bool>> ToExpression()
    {
        return x => x.FormVersionId == _formVersionId;
    }
}