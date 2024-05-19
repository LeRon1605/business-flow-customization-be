using BuildingBlocks.Domain.Events;
using BuildingBlocks.EventBus.Abstracts;
using BusinessFlow.Domain.SubmissionExecutionAggregate.DomainEvents;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using IntegrationEvents.BusinessFlow;

namespace BusinessFlow.Application.DomainEventHandlers;

public class SubmissionExecutionCreatedDomainEventHandler : IPersistedDomainEventHandler<SubmissionExecutionCreatedDomainEvent>
{
    private readonly ISubmissionExecutionRepository _submissionExecutionRepository;
    private readonly IEventPublisher _eventPublisher;
    
    public SubmissionExecutionCreatedDomainEventHandler(ISubmissionExecutionRepository submissionExecutionRepository
        , IEventPublisher eventPublisher)
    {
        _submissionExecutionRepository = submissionExecutionRepository;
        _eventPublisher = eventPublisher;
    }
    
    public async Task Handle(SubmissionExecutionCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        await _eventPublisher.Publish(new ExecutionSubmissionCreatedIntegrationEvent(@event.SubmissionExecution.Id
            , @event.SubmissionExecution.BusinessFlowBlockId), cancellationToken);
    }
}