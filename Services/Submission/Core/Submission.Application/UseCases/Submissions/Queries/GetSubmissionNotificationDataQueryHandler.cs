using Application.Dtos.Notifications.Responses;
using Application.Dtos.Submissions.Responses;
using AutoMapper;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using Submission.Application.UseCases.Submissions.Dtos;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Specifications;

namespace Submission.Application.UseCases.Submissions.Queries;

public class GetSubmissionNotificationDataQueryHandler : IQueryHandler<GetSubmissionNotificationDataQuery, List<SubmissionNotificationDataDto>>
{
    private readonly IBasicReadOnlyRepository<FormSubmission, int> _submissionRepository;
    private readonly IMapper _mapper;
    
    public GetSubmissionNotificationDataQueryHandler(IBasicReadOnlyRepository<FormSubmission, int> submissionRepository
        , IMapper mapper)
    {
        _submissionRepository = submissionRepository;
        _mapper = mapper;
    }
    
    public async Task<List<SubmissionNotificationDataDto>> Handle(GetSubmissionNotificationDataQuery request, CancellationToken cancellationToken)
    {
        var specification = new SubmissionByIdsSpecification(request.SubmissionIds);
        
        var submissions = await _submissionRepository.FilterAsync(specification, new BasicSubmissionQueryDto());
        return _mapper.Map<List<SubmissionNotificationDataDto>>(submissions);
    }
}