﻿using System.Linq.Expressions;
using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Repositories;

public interface IProjection<TEntity, TKey, TOut> where TEntity : IEntity<TKey> where TKey : IEquatable<TKey>
{
    Expression<Func<TEntity, TOut>> GetProject();
}