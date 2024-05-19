using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetSubmissionExecutionBusinessFlowQueryHandler : IQueryHandler<GetSubmissionExecutionBusinessFlowQuery, IList<SubmissionExecutionBusinessFlowDto>>
{
    private readonly IBasicReadOnlyRepository<SubmissionExecution, int> _submissionExecutionRepository;
    
    public GetSubmissionExecutionBusinessFlowQueryHandler(IBasicReadOnlyRepository<SubmissionExecution, int> submissionExecutionRepository)
    {
        _submissionExecutionRepository = submissionExecutionRepository;
    }
    
    public async Task<IList<SubmissionExecutionBusinessFlowDto>> Handle(GetSubmissionExecutionBusinessFlowQuery request, CancellationToken cancellationToken)
    {
        var specification = new SubmissionExecutionSpecification(request.SubmissionId);

        return await _submissionExecutionRepository.FilterAsync(specification,new SubmissionExecutionBusinessFlowDto());
    }
}