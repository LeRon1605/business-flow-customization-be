using Application.Dtos.SubmissionExecutions;
using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Queries;

public class GetAssignedSubmissionExecutionQueryHandler : IQueryHandler<GetAssignedSubmissionExecutionQuery, IList<AssignedSubmissionExecutionDto>>
{
    private readonly ISubmissionExecutionRepository _submissionExecutionRepository;
    private readonly ICurrentUser _currentUser;
    
    public GetAssignedSubmissionExecutionQueryHandler(ISubmissionExecutionRepository submissionExecutionRepository
        , ICurrentUser currentUser)
    {
        _submissionExecutionRepository = submissionExecutionRepository;
        _currentUser = currentUser;
    }

    public async Task<IList<AssignedSubmissionExecutionDto>> Handle(GetAssignedSubmissionExecutionQuery request, CancellationToken cancellationToken)
    {
        return await _submissionExecutionRepository.GetByPersonInChargeAsync(_currentUser.Id, new AssignedSubmissionExecutionQueryDto());
    }
}