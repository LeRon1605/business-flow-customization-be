using System.Linq.Expressions;
using Application.Dtos.SubmissionExecutions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class AssignedSubmissionExecutionQueryDto : AssignedSubmissionExecutionDto, IProjection<SubmissionExecution, AssignedSubmissionExecutionDto>
{
    public Expression<Func<SubmissionExecution, AssignedSubmissionExecutionDto>> GetProject()
    {
        return x => new AssignedSubmissionExecutionQueryDto()
        {
            Id = x.Id,
            SpaceId = x.BusinessFlowBlock.BusinessFlowVersion.SpaceId,
            SpaceName = x.BusinessFlowBlock.BusinessFlowVersion.Space.Name,
            SpaceColor = x.BusinessFlowBlock.BusinessFlowVersion.Space.Color,
            BusinessFlowId = x.BusinessFlowBlockId,
            BusinessFlowName = x.BusinessFlowBlock.Name,
            SubmissionId = x.SubmissionId
        };
    }
}