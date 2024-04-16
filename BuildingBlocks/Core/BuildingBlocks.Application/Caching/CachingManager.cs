using BuildingBlocks.Application.Caching.Interfaces;

namespace BuildingBlocks.Application.Caching;

public abstract class CachingManager<T> : ICachingManager where T : class
{
    protected const int DefaultCacheDurationInDay = 1;
    protected readonly string ResourceName;

    protected CachingManager()
    {
        ResourceName = typeof(T).Name;
    }
}