using System.Linq.Expressions;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Application.Schedulers;
using Hangfire;

namespace BuildingBlocks.Infrastructure.HangFire;

public class HangFireBackGroundJobManager : IBackGroundJobManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ICurrentUser _currentUser;
    private readonly IRecurringJobManager _recurringJobManager;
    
    public HangFireBackGroundJobManager(IBackgroundJobClient backgroundJobClient
        , IRecurringJobManager recurringJobManager
        , ICurrentUser currentUser)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
        _currentUser = currentUser;
    }
    
    public string Fire(Expression<Action> factory)
    {
        return _backgroundJobClient.Enqueue(factory);
    }

    public string Fire<T>(Expression<Action<T>> factory)
    {
        return _backgroundJobClient.Enqueue<T>(factory);
    }

    public string Fire(IBackGroundJob job)
    {
        if (_currentUser.IsAuthenticated)
        {
            job.UserId = _currentUser.Id;
            job.TenantId = _currentUser.TenantId;
        }
        
        return _backgroundJobClient.Enqueue<IBackGroundJobPublisher>(x => x.Publish(job));
    }

    public string FireAfter<T>(Expression<Action> factory, TimeSpan timeSpan)
    {
        return _backgroundJobClient.Schedule(factory, timeSpan);
    }

    public string FireAfter<T>(Expression<Action<T>> factory, TimeSpan timeSpan)
    {
        return _backgroundJobClient.Schedule(factory, timeSpan);
    }

    public string FireAfter(IBackGroundJob job, TimeSpan timeSpan)
    {
        if (_currentUser.IsAuthenticated)
        {
            job.UserId = _currentUser.Id;
            job.TenantId = _currentUser.TenantId;
        }
        
        return _backgroundJobClient.Schedule<IBackGroundJobPublisher>(x => x.Publish(job), timeSpan);
    }

    public string FireAt<T>(Expression<Action> factory, DateTime dateTime)
    {
        return _backgroundJobClient.Schedule(factory, new DateTimeOffset(dateTime));
    }

    public string FireAt<T>(Expression<Action<T>> factory, DateTime dateTime)
    {
        return _backgroundJobClient.Schedule<T>(factory, new DateTimeOffset(dateTime));
    }
    
    public string FireAt(IBackGroundJob job, DateTime dateTime)
    {
        if (_currentUser.IsAuthenticated)
        {
            job.UserId = _currentUser.Id;
            job.TenantId = _currentUser.TenantId;
        }
        
        return _backgroundJobClient.Schedule<IBackGroundJobPublisher>(x => x.Publish(job), new DateTimeOffset(dateTime));
    }

    public bool Remove(string jobId)
    {
        return _backgroundJobClient.Delete(jobId);
    }
}