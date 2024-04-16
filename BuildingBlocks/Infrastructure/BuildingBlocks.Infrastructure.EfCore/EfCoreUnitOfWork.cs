using BuildingBlocks.Application.Data;
using BuildingBlocks.Domain.Events;
using BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServeSync.Domain.SeedWorks.Events;

namespace BuildingBlocks.Infrastructure.EfCore;

public class EfCoreUnitOfWork : IUnitOfWork 
{
    private readonly DbContext _dbContext;
    private IDbContextTransaction? _transaction;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EfCoreUnitOfWork> _logger;
    private readonly List<IDomainEvent> _domainEvents = new();
    private readonly IDbContextSaveChangesBehaviour _dbContextSaveChangesBehaviour;
    
    public EfCoreUnitOfWork(DbContextFactory dbContextFactory
        , IDbContextSaveChangesBehaviour dbContextSaveChangesBehaviour
        , ILogger<EfCoreUnitOfWork> logger
        , IServiceProvider serviceProvider)
    {
        _dbContext = dbContextFactory.DbContext;
        _dbContextSaveChangesBehaviour = dbContextSaveChangesBehaviour;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task CommitAsync()
    {
        var domainEvents = await _dbContextSaveChangesBehaviour.SaveChangesAsync(_dbContext);

        if (_transaction != null)
        {
            _domainEvents.AddRange(domainEvents);
        }
        else
        {
            await DispatchPersistedDomainEventsAsync(domainEvents);    
        }
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(bool autoRollbackOnFail)
    {
        if (_transaction != null)
        {
            try
            {
                await CommitAsync();
                await _transaction.CommitAsync();

                if (_domainEvents.Any())
                {
                    await DispatchPersistedDomainEventsAsync(_domainEvents);
                    _domainEvents.Clear();
                }
                
                await _transaction.DisposeAsync();
            }
            catch(Exception e)
            {
                _domainEvents.Clear();
                _logger.LogError("Commit transaction failed: {Message}", e.Message);
                if (autoRollbackOnFail)
                {
                    await RollbackTransactionAsync();
                }
                else
                {
                    throw;
                }
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
    
    private async Task DispatchPersistedDomainEventsAsync(IList<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("Dispatching persisted domain event: {EventName}", domainEvent.GetType().Name);
            
            var domainEventHandlerType = typeof(IPersistedDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var domainEventHandlers = _serviceProvider.GetServices(domainEventHandlerType);
            
            foreach (var domainEventHandler in domainEventHandlers)
            {
                var handleMethod = domainEventHandlerType.GetMethod(nameof(IPersistedDomainEventHandler<IDomainEvent>.Handle));
                await (Task) handleMethod!.Invoke(domainEventHandler, new object[] { domainEvent, default(CancellationToken) });
            }
        }
    }
}