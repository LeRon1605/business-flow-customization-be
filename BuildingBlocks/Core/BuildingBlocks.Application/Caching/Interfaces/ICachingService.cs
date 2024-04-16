using BuildingBlocks.Kernel.Services;

namespace BuildingBlocks.Application.Caching.Interfaces;

public interface ICachingService : IScopedService
{
    Task<T> GetOrAddAsync<T>(string id, Func<Task<T>> factory, TimeSpan expireTime);

    Task SetRecordAsync<T>(string id, T data, TimeSpan expireTime);

    Task<T> GetRecordAsync<T>(string id);

    Task RemoveRecordAsync(string id);
}