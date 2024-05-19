using BuildingBlocks.Application.Data;
using BuildingBlocks.EventBus;
using IntegrationEvents.BusinessFlow;
using Submission.Domain.FormAggregate.Exceptions;
using Submission.Domain.FormAggregate.Repositories;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.IntegrationEvents.Handlers;

public class ExecutionSubmissionCreatedIntegrationEventHandler : IntegrationEventHandler<ExecutionSubmissionCreatedIntegrationEvent>
{
    private readonly IFormRepository _formRepository;
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public ExecutionSubmissionCreatedIntegrationEventHandler(IServiceProvider serviceProvider
        , IFormRepository formRepository
        , ISubmissionDomainService submissionDomainService
        , IUnitOfWork unitOfWork) : base(serviceProvider)
    {
        _formRepository = formRepository;
        _submissionDomainService = submissionDomainService;
        _unitOfWork = unitOfWork;
    }

    public override async Task HandleAsync(ExecutionSubmissionCreatedIntegrationEvent @event)
    {
        var form = await _formRepository.FindByBusinessFlowBlockIdAsync(@event.BusinessFlowBlockId);
        if (form == null)
        {
            throw new BusinessFlowBlockFormNotFoundException(@event.BusinessFlowBlockId);
        }

        var submissionModel = new SubmissionModel()
        {
            Name = form.Name,
            FormVersionId = form.Versions.Last().Id,
            Fields = new List<SubmissionFieldModel>()
        };
        
        await _submissionDomainService.CreateAsync(form.SpaceId, @event.ExecutionId, submissionModel);
        await _unitOfWork.CommitAsync();
    }
}