﻿using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specifications.Interfaces;

namespace BuildingBlocks.Domain.Specifications;

public abstract class PagingAndSortingSpecification<TEntity, TKey> : Specification<TEntity, TKey>, IPagingAndSortingSpecification<TEntity, TKey>
    where TEntity : IEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    public int Take { get; set; }
    public int Skip { get; set; }
    public string? Sorting { get; set; }

    protected PagingAndSortingSpecification(int page, int size, string? sorting, bool isTracking = false) : base(isTracking)
    {
        Skip = (page - 1) * size;
        Take = size;
        Sorting = sorting;
    }

    protected PagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        Take = specification.Take;
        Skip = specification.Skip;
        Sorting = specification.BuildSorting();
        IncludeExpressions = specification.IncludeExpressions;
        IncludeStrings = specification.IncludeStrings;
    }
    
    protected PagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity, TKey> left, ISpecification<TEntity, TKey> right)
    {
        Take = left.Take;
        Skip = left.Skip;
        Sorting = left.BuildSorting();
        IncludeExpressions = left.IncludeExpressions;
        IncludeStrings = left.IncludeStrings;
        
        IncludeExpressions.AddRange(right.IncludeExpressions);
        IncludeStrings.AddRange(right.IncludeStrings);
    }

    public new IPagingAndSortingSpecification<TEntity, TKey> AndIf(bool condition, ISpecification<TEntity, TKey> specification)
    {
        if (!condition)
        {
            return this;
        }
        
        return new AndPagingAndSortingSpecification<TEntity, TKey>(this, specification);
    }
    
    public new IPagingAndSortingSpecification<TEntity, TKey> And(ISpecification<TEntity, TKey> specification)
    {
        return new AndPagingAndSortingSpecification<TEntity, TKey>(this, specification);
    }
    
    public virtual string? BuildSorting()
    {
        return Sorting;
    }
}

public abstract class PagingAndSortingSpecification<TEntity> : PagingAndSortingSpecification<TEntity, int>, IPagingAndSortingSpecification<TEntity>
    where TEntity : IEntity
{
    public PagingAndSortingSpecification(int page, int size, string? sorting, bool isTracking = false) : base(page, size, sorting, isTracking)
    {
    }

    public PagingAndSortingSpecification(IPagingAndSortingSpecification<TEntity> specification) : base(specification)
    {
    }
}