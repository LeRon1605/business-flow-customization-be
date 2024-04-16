using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Specifications.Interfaces;

public interface ISpecification<TEntity, TKey> 
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey> 
{
    bool IsTracking { get; set; }

    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }

    List<string> IncludeStrings { get; set; }

    bool IsSatisfiedBy(TEntity entity);

    ISpecification<TEntity, TKey> AndIf(bool condition, ISpecification<TEntity, TKey> specification);
    
    ISpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification);

    Expression<Func<TEntity, bool>> ToExpression();
}

public interface ISpecification<TEntity> : ISpecification<TEntity, int> 
    where TEntity : IEntity
{
}