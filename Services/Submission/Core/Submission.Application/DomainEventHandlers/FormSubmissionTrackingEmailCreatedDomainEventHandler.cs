using BuildingBlocks.Domain.Events;
using BuildingBlocks.EventBus.Abstracts;
using IntegrationEvents.Hub;
using Microsoft.AspNetCore.WebUtilities;
using Submission.Application.Services.Dtos;
using Submission.Domain.SubmissionAggregate.DomainEvents;

namespace Submission.Application.DomainEventHandlers;

public class FormSubmissionTrackingEmailCreatedDomainEventHandler : IPersistedDomainEventHandler<FormSubmissionTrackingEmailCreatedDomainEvent>
{
    private readonly IEventPublisher _eventPublisher;
    private readonly PublicFormSetting _publicFormSetting;
    
    public FormSubmissionTrackingEmailCreatedDomainEventHandler(IEventPublisher eventPublisher
        , PublicFormSetting publicFormSetting)
    {
        _eventPublisher = eventPublisher;
        _publicFormSetting = publicFormSetting;
    }
    
    public async Task Handle(FormSubmissionTrackingEmailCreatedDomainEvent @event, CancellationToken cancellationToken)
    {
        await _eventPublisher.Publish(new EmailSenderIntegrationEvent(@event.FormSubmission.Name
                , @event.FormSubmission.TrackingEmail!
                , "FormSubmissionTracking"
                , new
                {
                    FormSubmissionName = @event.FormSubmission.Name,
                    CallBackUrl = QueryHelpers.AddQueryString(_publicFormSetting.TrackingUrl, new Dictionary<string, string>()
                    {
                        { "Token", @event.FormSubmission.TrackingToken! }
                    })
                })
            , cancellationToken);
    }
}