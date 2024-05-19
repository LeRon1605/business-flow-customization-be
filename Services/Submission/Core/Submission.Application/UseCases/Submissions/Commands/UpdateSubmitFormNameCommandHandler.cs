using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Repositories;

namespace Submission.Application.UseCases.Submissions.Commands;

public class UpdateSubmitFormNameCommandHandler : ICommandHandler<UpdateSubmitFormNameCommand>
{
    private readonly IFormSubmissionRepository _formSubmissionRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateSubmitFormNameCommandHandler(IFormSubmissionRepository formSubmissionRepository
        , IUnitOfWork unitOfWork)
    {
        _formSubmissionRepository = formSubmissionRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateSubmitFormNameCommand request, CancellationToken cancellationToken)
    {
        var submission = await _formSubmissionRepository.FindByIdAsync(request.SubmissionId);
        if (submission == null)
        {
            throw new SubmissionNotFoundException(request.SubmissionId);
        }
        
        submission.UpdateName(request.Name);
        
        _formSubmissionRepository.Update(submission);
        await _unitOfWork.CommitAsync();
    }
}