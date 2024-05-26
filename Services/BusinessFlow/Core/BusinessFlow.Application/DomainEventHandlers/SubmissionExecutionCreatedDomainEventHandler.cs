using BuildingBlocks.Application.Schedulers;
using BuildingBlocks.Domain.Events;
using BusinessFlow.Application.UseCases.BusinessFlows.Jobs;
using BusinessFlow.Domain.SubmissionExecutionAggregate.DomainEvents;

namespace BusinessFlow.Application.DomainEventHandlers;

public class SubmissionExecutionCreatedDomainEventHandler : IPersistedDomainEventHandler<SubmissionExecutionCreatedDomainEvent>
{
    private readonly IBackGroundJobManager _backGroundJobManager;
    
    public SubmissionExecutionCreatedDomainEventHandler(IBackGroundJobManager backGroundJobManager)
    {
        _backGroundJobManager = backGroundJobManager;
    }
    
    public Task Handle(SubmissionExecutionCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        _backGroundJobManager.Fire(new SubmissionExecutionPublishIntegrationEventJob(@event.SubmissionExecution.Id));
        return Task.CompletedTask;
    }
}