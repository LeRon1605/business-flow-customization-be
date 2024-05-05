using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreRepository<TAggregateRoot, TKey> : EfCoreReadOnlyRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
    public EfCoreRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public void Delete(TAggregateRoot entity)
    {
        DbSet.Remove(entity);
    }

    public virtual async Task InsertAsync(TAggregateRoot entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Update(TAggregateRoot entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Insert(TAggregateRoot entity)
    {
        DbSet.Add(entity);
    }

    public virtual Task InsertRangeAsync(IEnumerable<TAggregateRoot> entities)
    {
        return DbSet.AddRangeAsync(entities);
    }
}

public class EfCoreRepository<TAggregateRoot> : EfCoreRepository<TAggregateRoot, int>
    where TAggregateRoot : class, IAggregateRoot
{
    public EfCoreRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}