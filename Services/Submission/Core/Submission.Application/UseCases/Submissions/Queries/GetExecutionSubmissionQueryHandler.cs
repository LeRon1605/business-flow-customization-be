using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Specifications;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetExecutionSubmissionQueryHandler : IQueryHandler<GetExecutionSubmissionQuery, SubmissionDto>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _formSubmissionRepository;
    
    public GetExecutionSubmissionQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> formSubmissionRepository)
    {
        _formSubmissionRepository = formSubmissionRepository;
    }
    
    public async Task<SubmissionDto> Handle(GetExecutionSubmissionQuery request, CancellationToken cancellationToken)
    {
        var submission = await _formSubmissionRepository.FindAsync(new SubmissionByExecutionSpecification(request.ExecutionId), new SubmissionDto());
        if (submission == null)
        {
            throw new ExecutionSubmissionNotFoundException(request.ExecutionId);
        }
        
        return submission;
    }
}