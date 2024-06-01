using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionDataQueryHandler : IQueryHandler<GetSubmissionDataQuery, IList<BasicSubmissionDto>>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _submissionRepository;

    public GetSubmissionDataQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }

    public async Task<IList<BasicSubmissionDto>> Handle(GetSubmissionDataQuery request, CancellationToken cancellationToken)
    {
        return await _submissionRepository.FindByIncludedIdsAsync(request.SubmissionIds, new BasicSubmissionQueryDto());
    }
}