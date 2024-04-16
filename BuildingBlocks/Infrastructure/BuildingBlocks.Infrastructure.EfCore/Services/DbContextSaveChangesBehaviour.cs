using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.EfCore.Services;

public class DbContextSaveChangesBehaviour : IDbContextSaveChangesBehaviour
{
    private readonly IEntityStateDetector _entityStateDetector;
    private readonly IMediator _mediator;
    private readonly ILogger<DbContextSaveChangesBehaviour> _logger;
    
    public DbContextSaveChangesBehaviour(IEntityStateDetector entityStateDetector
        , IMediator mediator
        , ILogger<DbContextSaveChangesBehaviour> logger)
    {
        _entityStateDetector = entityStateDetector;
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task<IList<IDomainEvent>> SaveChangesAsync(DbContext dbContext)
    {
        _entityStateDetector.ProcessEntityState(dbContext.ChangeTracker);
        
        var domainEvents = await DispatchDomainEventsAsync(dbContext);

        await dbContext.SaveChangesAsync();
        
        return domainEvents;
    }
    
    private async Task<IList<IDomainEvent>> DispatchDomainEventsAsync(DbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker.Entries<IDomainModel>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .Where(x => x.Entity.DomainEvents != null)
            .SelectMany(x => x.Entity.DomainEvents!)
            .Distinct()
            .ToList();
        
        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("Dispatching domain event: {EventName}", domainEvent.GetType().Name);
            await _mediator.Publish(domainEvent);   
        }
        
        return domainEvents;
    }
}