using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

namespace Submission.Application.UseCases.Submissions.Commands;

public class DeleteSubmitFormCommandHandler : ICommandHandler<DeleteSubmitFormCommand>
{
    private readonly ISubmissionDomainService _submissionDomainService;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteSubmitFormCommandHandler(ISubmissionDomainService submissionDomainService
        , IUnitOfWork unitOfWork)
    {
        _submissionDomainService = submissionDomainService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteSubmitFormCommand request, CancellationToken cancellationToken)
    {
        await _submissionDomainService.DeleteAsync(request.SubmissionId);
        await _unitOfWork.CommitAsync();
    }
}