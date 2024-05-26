using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.MongoDb.Repositories;
using Hub.Domain.NotificationAggregate.Entities;
using Hub.Domain.NotificationAggregate.Enums;
using Hub.Domain.NotificationAggregate.Repositories;
using MongoDB.Driver;

namespace Hub.Infrastructure.MongoDb.Repositories;

public class NotificationRepository : MongoDbRepository<Notification, Guid>, INotificationRepository
{
    public NotificationRepository(IMongoDatabase database, ICurrentUser currentUser) : base(database, currentUser)
    {
    }

    public Task<List<Notification>> GetPagedAsync(int page, int pageSize, string receiverId)
    {
        var filter = Builders<Notification>.Filter.Eq(x => x.ReceiverId, receiverId)
            & Builders<Notification>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId);
        var sort = Builders<Notification>.Sort.Descending(x => x.Created);
        
        return Collection.Find(filter)
            .Sort(sort)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync(string receiverId)
    {
        var filter = Builders<Notification>.Filter.Eq(x => x.ReceiverId, receiverId)
            & Builders<Notification>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId);
        
        var total = await Collection.CountDocumentsAsync(filter);
        return (int)total;
    }

    public async Task<int> GetUnReadCountAsync(string receiverId)
    {
        var filter = Builders<Notification>.Filter.Eq(x => x.ReceiverId, receiverId)
            & Builders<Notification>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId)
            & Builders<Notification>.Filter.Eq(x => x.Status, NotificationStatus.UnRead);
        
        var total = await Collection.CountDocumentsAsync(filter);
        return (int)total;
    }

    public Task MarkReadAsync(Guid id, string receiverId)
    {
        var filter = Builders<Notification>.Filter.Eq(x => x.Id, id)
            & Builders<Notification>.Filter.Eq(x => x.ReceiverId, receiverId)
            & Builders<Notification>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId);
        
        var update = Builders<Notification>.Update
            .Set(x => x.Status, NotificationStatus.Read);
        
        return Collection.UpdateOneAsync(filter, update);
    }

    public Task MarkAllReadAsync(string receiverId)
    {
        var filter = Builders<Notification>.Filter.Eq(x => x.ReceiverId, receiverId)
            & Builders<Notification>.Filter.Eq(x => x.TenantId, CurrentUser.TenantId)
            & Builders<Notification>.Filter.Eq(x => x.Status, NotificationStatus.UnRead);
        
        var update = Builders<Notification>.Update
            .Set(x => x.Status, NotificationStatus.Read);
        
        return Collection.UpdateManyAsync(filter, update);
    }
}