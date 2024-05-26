using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specifications.Interfaces;

namespace BuildingBlocks.Domain.Specifications;

public abstract class Specification<TEntity, TKey> : ISpecification<TEntity, TKey> 
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey> 
{
    public bool IsTracking { get; set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }
    public List<string> IncludeStrings { get; set; }

    protected Specification(bool isTracking = false)
    {
        IsTracking = isTracking;
        IncludeExpressions = new List<Expression<Func<TEntity, object>>>();
        IncludeStrings = new List<string>();
    }

    protected Specification(ISpecification<TEntity, TKey> specification)
    {
        IncludeExpressions = specification.IncludeExpressions;
        IncludeStrings = specification.IncludeStrings;
    }
    
    protected Specification(ISpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
    {
        IncludeExpressions = left.IncludeExpressions;
        IncludeStrings = left.IncludeStrings;
        
        IncludeExpressions.AddRange(right.IncludeExpressions);
        IncludeStrings.AddRange(right.IncludeStrings);
    }

    public bool IsSatisfiedBy(TEntity entity)
    {
        return ToExpression().Compile().Invoke(entity);
    }
    
    public ISpecification<TEntity, TKey> AndIf(bool condition, ISpecification<TEntity, TKey> specification)
    {
        if (!condition)
        {
            return this;
        }
        
        return new AndSpecification<TEntity, TKey>(this, specification);
    }
    
    public ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification)
    {
        return new AndSpecification<TEntity, TKey>(this, specification);
    }

    public void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        IncludeExpressions.Add(expression);
    }

    public void AddInclude(string prop)
    {
        IncludeStrings.Add(prop);
    }

    public abstract Expression<Func<TEntity, bool>> ToExpression();
}

public abstract class Specification<TEntity> : Specification<TEntity, int>
    where TEntity : IEntity
{
}

public class EmptySpecification<TEntity, TKey> : Specification<TEntity, TKey>
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey> 
{
    public static ISpecification<TEntity, TKey> Instance { get; } = new EmptySpecification<TEntity, TKey>();
    
    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        return x => true;
    }
}

public class EmptyFalseSpecification<TEntity, TKey> : Specification<TEntity, TKey>
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey> 
{
    public static ISpecification<TEntity, TKey> Instance { get; } = new EmptyFalseSpecification<TEntity, TKey>();
    
    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        return x => false;
    }
}