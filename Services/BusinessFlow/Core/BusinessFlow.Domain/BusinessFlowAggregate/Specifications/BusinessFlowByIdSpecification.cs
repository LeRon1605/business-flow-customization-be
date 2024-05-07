using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

public class BusinessFlowByIdSpecification : Specification<BusinessFlowVersion>
{
    private readonly int _id;
    
    public BusinessFlowByIdSpecification(int id)
    {
        _id = id;
    }
    
    public override Expression<Func<BusinessFlowVersion, bool>> ToExpression()
    {
        return x => x.Id == _id;
    }
}