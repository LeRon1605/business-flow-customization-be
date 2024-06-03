using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using MediatR;
using MongoDB.Driver;

namespace BuildingBlocks.Infrastructure.MongoDb.Repositories;
 
public class MongoDbRepository<TAggregateRoot, TKey> : MongoDbReadOnlyRepository<TAggregateRoot, TKey>, IRepository<TAggregateRoot, TKey>
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly IMediator _mediator;
    
    public MongoDbRepository(IMongoDatabase database
        , ICurrentUser currentUser
        , IMediator mediator) : base(database, currentUser)
    {
        _mediator = mediator;
    }

    public async Task InsertAsync(TAggregateRoot entity)
    {
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasAuditable)))
        {
            ((IHasAuditable)entity).Create(CurrentUser.Id);
        }
        
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasTenant)))
        {
            ((IHasTenant)entity).TenantId = CurrentUser.TenantId;
        }

        await Collection.InsertOneAsync(entity);
        
        if (entity.DomainEvents == null || !entity.DomainEvents.Any())
            return;
        
        foreach (var domainEvent in entity.DomainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }

    public Task InsertRangeAsync(IEnumerable<TAggregateRoot> entities)
    {
        var entitiesList = entities.ToList();
        
        foreach (var entity in entitiesList)
        {
            if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasAuditable)))
            {
                ((IHasAuditable)entity).Create(CurrentUser.Id);
            }
            
            if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasTenant)))
            {
                ((IHasTenant)entity).TenantId = CurrentUser.TenantId;
            }
        }
        
        return Collection.InsertManyAsync(entitiesList);
    }

    public void Insert(TAggregateRoot entity)
    {
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasAuditable)))
        {
            ((IHasAuditable)entity).Create(CurrentUser.Id);
        }
        
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasTenant)))
        {
            ((IHasTenant)entity).TenantId = CurrentUser.TenantId;
        }
        
        Collection.InsertOne(entity);
    }

    public void Delete(TAggregateRoot entity)
    {
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasSoftDelete)))
        {
            ((IHasSoftDelete)entity).Delete(CurrentUser.Id);
            Update(entity);
            return;
        }
        
        Collection.DeleteOne(x => x.Id.Equals(entity.Id));
    }

    public void Update(TAggregateRoot entity)
    {
        if (typeof(TAggregateRoot).IsAssignableTo(typeof(IHasAuditable)))
        {
            ((IHasAuditable)entity).Update(CurrentUser.Id);
        }
        
        Collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity);
    }
}