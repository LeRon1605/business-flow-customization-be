using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Specifications;

public class PublishedBusinessFlowVersionSpecification : Specification<BusinessFlowVersion>
{
    public override Expression<Func<BusinessFlowVersion, bool>> ToExpression()
    {
        return x => x.Status == BusinessFlowStatus.Published;
    }
}