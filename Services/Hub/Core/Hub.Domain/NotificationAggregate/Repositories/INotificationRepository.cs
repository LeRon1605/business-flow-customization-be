using BuildingBlocks.Domain.Repositories;
using Hub.Domain.NotificationAggregate.Entities;

namespace Hub.Domain.NotificationAggregate.Repositories;

public interface INotificationRepository : IRepository<Notification, Guid>
{
    Task<List<Notification>> GetPagedAsync(int page, int pageSize, string receiverId);
    
    Task<int> GetCountAsync(string receiverId);
    
    Task<int> GetUnReadCountAsync(string receiverId);
    
    Task MarkReadAsync(Guid id, string receiverId);
    
    Task MarkAllReadAsync(string receiverId);
}