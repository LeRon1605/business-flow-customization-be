using BuildingBlocks.Application.Cqrs;
using BuildingBlocks.Application.Data;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateExecutionTaskStatusCommandHandler : ICommandHandler<UpdateExecutionTaskStatusCommand>
{
    private readonly ISubmissionExecutionRepository _submissionExecutionRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateExecutionTaskStatusCommandHandler(ISubmissionExecutionRepository submissionExecutionRepository
        , IUnitOfWork unitOfWork)
    {
        _submissionExecutionRepository = submissionExecutionRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateExecutionTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var execution = await _submissionExecutionRepository.FindByIdAsync(request.ExecutionId);
        if (execution == null)
        {
            throw new SubmissionExecutionNotFoundException(request.ExecutionId);
        }
        
        execution.SetTaskStatus(request.TaskId, request.Status);

        _submissionExecutionRepository.Update(execution);
        await _unitOfWork.CommitAsync();
    }
}