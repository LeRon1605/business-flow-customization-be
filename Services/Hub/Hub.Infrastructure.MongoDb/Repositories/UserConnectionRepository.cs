using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.MongoDb.Repositories;
using Hub.Domain.ConnectionAggregate.Entities;
using Hub.Domain.ConnectionAggregate.Repositories;
using MongoDB.Driver;

namespace Hub.Infrastructure.MongoDb.Repositories;

public class UserConnectionRepository : MongoDbRepository<UserConnection, int>, IUserConnectionRepository
{
    public UserConnectionRepository(IMongoDatabase database, ICurrentUser currentUser) : base(database, currentUser)
    {
    }
}