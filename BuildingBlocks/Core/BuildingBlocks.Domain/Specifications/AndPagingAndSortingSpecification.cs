using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specifications.Interfaces;

namespace BuildingBlocks.Domain.Specifications;

public class AndPagingAndSortingSpecification<TEntity, TKey> : PagingAndSortingSpecification<TEntity, TKey>, IPagingAndSortingSpecification<TEntity, TKey>
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    private readonly IPagingAndSortingSpecification<TEntity, TKey> _left;

    private readonly ISpecification<TEntity, TKey> _right;

    public AndPagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right) : base(left, right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var parameter = Expression.Parameter(typeof(TEntity));
        var body = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter)
        );

        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }
}

public abstract class AndPagingAndSortingSpecification<TEntity> : PagingAndSortingSpecification<TEntity, int>, IPagingAndSortingSpecification<TEntity>
    where TEntity : IEntity
{
    public AndPagingAndSortingSpecification(int page, int size, string? sorting, bool isTracking = false) : base(page, size, sorting, isTracking)
    {
    }

    public AndPagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, int> specification) : base(specification)
    {
    }
}