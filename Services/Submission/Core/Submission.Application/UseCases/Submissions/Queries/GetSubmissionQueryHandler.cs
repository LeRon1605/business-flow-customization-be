using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Dtos;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Specifications;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionQueryHandler : IQueryHandler<GetSubmissionQuery, PagedResultDto<SubmissionDto>>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _submissionRepository;
    
    public GetSubmissionQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }
    
    public async Task<PagedResultDto<SubmissionDto>> Handle(GetSubmissionQuery request, CancellationToken cancellationToken)
    {
        var specification = new SubmissionBySpacePagingSpecification(request.Page, request.Size, request.Sorting, request.SpaceId)
            .And(new SubmissionByVersionSpecification(request.FormVersionId));
        
        var submissions = await _submissionRepository.GetPagedListAsync(specification, new SubmissionDto());
        var total = await _submissionRepository.GetCountAsync(specification);
        
        return new PagedResultDto<SubmissionDto>(total, request.Size, submissions);
    }
}