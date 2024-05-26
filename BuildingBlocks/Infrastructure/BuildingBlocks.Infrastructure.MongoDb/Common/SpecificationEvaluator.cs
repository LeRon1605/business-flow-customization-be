using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specifications.Interfaces;
using MongoDB.Driver.Linq;

namespace BuildingBlocks.Infrastructure.MongoDb.Common;

public static class SpecificationEvaluator<TEntity, TKey> 
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public static IMongoQueryable<TEntity> GetQuery(IMongoQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification)
    {
        var queryable = inputQuery;
        return queryable.Where(specification.ToExpression());
    }

    public static IMongoQueryable<TEntity> GetQuery(IMongoQueryable<TEntity> inputQuery, IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        var queryable = GetQuery(inputQuery, (ISpecification<TEntity, TKey>)specification);

        var specificationSorting = specification.BuildSorting();
        if (string.IsNullOrWhiteSpace(specificationSorting))
            return queryable.Skip(specification.Skip).Take(specification.Take);
            
        // if (typeof(TEntity).IsAssignableTo(typeof(IHasAuditable)))
        // {
        //     if (string.IsNullOrWhiteSpace(specificationSorting))
        //     {
        //         queryable = queryable.OrderBy(GetDefaultSorting());  
        //     }
        //     else
        //     {
        //         queryable = queryable.OrderBy($"{specificationSorting}, {GetDefaultSorting()}");
        //     }
        // }
        // else
        // {
        //     if (!string.IsNullOrWhiteSpace(specificationSorting))
        //     {
        //         queryable = queryable.OrderBy(specificationSorting);    
        //     }
        // }

        return queryable.Skip(specification.Skip).Take(specification.Take);
    }
    
    private static string GetDefaultSorting()
    {
        if (typeof(TEntity).IsAssignableTo(typeof(IHasAuditable)))
        {
            return $"{nameof(IAuditableEntity.Created)} desc";
        }

        return string.Empty;
    }
}