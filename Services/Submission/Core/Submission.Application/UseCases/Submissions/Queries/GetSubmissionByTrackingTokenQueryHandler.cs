using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Repositories;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionByTrackingTokenQueryHandler : IQueryHandler<GetSubmissionByTrackingTokenQuery, SubmissionDto>
{
    private readonly IFormSubmissionRepository _submissionRepository;
    
    public GetSubmissionByTrackingTokenQueryHandler(IFormSubmissionRepository submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }

    public async Task<SubmissionDto> Handle(GetSubmissionByTrackingTokenQuery request, CancellationToken cancellationToken)
    {
        var submission = await _submissionRepository.FindByTrackingTokenAsync(request.TrackingToken, new SubmissionDto());
        if (submission == null)
        {
            throw new SubmissionNotFoundException(request.TrackingToken);
        }
        
        return submission;
    }
}