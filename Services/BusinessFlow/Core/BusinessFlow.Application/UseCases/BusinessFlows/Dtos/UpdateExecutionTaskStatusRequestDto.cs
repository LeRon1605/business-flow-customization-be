using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class UpdateExecutionTaskStatusRequestDto
{
    public SubmissionExecutionTaskStatus Status { get; set; }
}