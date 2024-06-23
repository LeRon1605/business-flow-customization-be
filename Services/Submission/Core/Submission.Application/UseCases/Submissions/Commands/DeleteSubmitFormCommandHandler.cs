using BuildingBlocks.Application.Cqrs;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

namespace Submission.Application.UseCases.Submissions.Commands;

public class DeleteSubmitFormCommandHandler : ICommandHandler<DeleteSubmitFormCommand>
{
    private readonly ISubmissionDomainService _submissionDomainService;
    
    public DeleteSubmitFormCommandHandler(ISubmissionDomainService submissionDomainService)
    {
        _submissionDomainService = submissionDomainService;
    }
    
    public Task Handle(DeleteSubmitFormCommand request, CancellationToken cancellationToken)
    {
        return _submissionDomainService.DeleteAsync(request.SubmissionId);
    }
}