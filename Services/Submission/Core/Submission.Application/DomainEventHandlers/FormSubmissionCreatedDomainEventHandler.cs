using BuildingBlocks.Domain.Events;
using BuildingBlocks.EventBus.Abstracts;
using IntegrationEvents.FormSubmissions;
using Submission.Domain.SubmissionAggregate.DomainEvents;

namespace Submission.Application.DomainEventHandlers;

public class FormSubmissionCreatedDomainEventHandler : IPersistedDomainEventHandler<FormSubmissionCreatedDomainEvent>
{
    private readonly IEventPublisher _eventPublisher;
    
    public FormSubmissionCreatedDomainEventHandler(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }
    
    public Task Handle(FormSubmissionCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        var integrationEvent = new FormSubmissionCreatedIntegrationEvent(@event.FormSubmission.Id, @event.FormSubmission.BusinessFlowVersionId);
        return _eventPublisher.Publish(integrationEvent, cancellationToken);
    }
}