using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Repositories;
using MongoDB.Driver;

namespace BuildingBlocks.Infrastructure.MongoDb.Repositories;

public class MongoDbReadOnlyRepository<TAggregateRoot, TKey> : MongoDbBasicReadOnlyRepository<TAggregateRoot, TKey>, IReadOnlyRepository<TAggregateRoot, TKey>
    where TAggregateRoot : class, IAggregateRoot<TKey>
    where TKey : IEquatable<TKey>
{
    public MongoDbReadOnlyRepository(IMongoDatabase database, ICurrentUser currentUser) : base(database, currentUser)
    {
    }
}