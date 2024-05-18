using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Specifications;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionByIdQueryHandler : IQueryHandler<GetSubmissionByIdQuery, SubmissionDto>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _submissionRepository;
    
    public GetSubmissionByIdQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }
    
    public async Task<SubmissionDto> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new SubmissionBySpaceSpecification(request.SpaceId)
            .And(new SubmissionByVersionSpecification(request.FormVersionId))
            .And(new SubmissionByIdSpecification(request.Id));
        
        var submission = await _submissionRepository.FindAsync(specification, new SubmissionDto());
        if (submission == null)
        {
            throw new SubmissionNotFoundException(request.Id);
        }
        
        return submission;
    }
}