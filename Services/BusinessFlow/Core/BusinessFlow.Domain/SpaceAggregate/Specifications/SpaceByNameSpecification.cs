using System.Linq.Expressions;
using BuildingBlocks.Domain.Specifications;
using BusinessFlow.Domain.SpaceAggregate.Entities;

namespace BusinessFlow.Domain.SpaceAggregate.Specifications;

public class SpaceByNameSpecification : Specification<Space, int>
{
    private readonly string _name;
    
    public SpaceByNameSpecification(string name)
    {
        _name = name;
    }
    
    public override Expression<Func<Space, bool>> ToExpression()
    {
        return x => x.Name == _name;
    }
}