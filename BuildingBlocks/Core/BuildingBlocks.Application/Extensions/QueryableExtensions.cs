﻿using System.Linq.Expressions;

namespace BuildingBlocks.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> query, 
        bool condition, 
        Expression<Func<T, bool>> predicate)
        => condition ? query.Where(predicate) : query;
}