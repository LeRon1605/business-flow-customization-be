using BuildingBlocks.Application.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Application.Schedulers;

public class BackGroundJobPublisher : IBackGroundJobPublisher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<BackGroundJobPublisher> _logger;
    
    public BackGroundJobPublisher(IServiceProvider serviceProvider, ILogger<BackGroundJobPublisher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task Publish(IBackGroundJob job)
    {
        var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();
        
        if (!string.IsNullOrEmpty(job.UserId) && job.TenantId.HasValue)
        {
            currentUser.IsAuthenticated = true;
            currentUser.Id = job.UserId;
            currentUser.TenantId = job.TenantId.Value;
        }
        
        _logger.LogInformation("Job {JobName} published!", job.GetType().Name);
        try
        {
            await mediator.Publish(job);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while publishing job {JobName}", job.GetType().Name);
        }
    }
}