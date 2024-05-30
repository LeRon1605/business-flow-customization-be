using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Exceptions;
using BusinessFlow.Domain.BusinessFlowAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Exceptions;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Specifications;

namespace BusinessFlow.Domain.SubmissionExecutionAggregate.DomainServices;

public class BusinessFlowExecutorDomainService : IBusinessFlowExecutorDomainService
{
    private readonly ISubmissionExecutionRepository _submissionExecutionRepository;
    private readonly IBusinessFlowVersionRepository _businessFlowVersionRepository;
    private readonly IBusinessFlowBlockRepository _businessFlowBlockRepository;
    
    public BusinessFlowExecutorDomainService(ISubmissionExecutionRepository submissionExecutionRepository
        , IBusinessFlowVersionRepository businessFlowVersionRepository
        , IBusinessFlowBlockRepository businessFlowBlockRepository)
    {
        _submissionExecutionRepository = submissionExecutionRepository;
        _businessFlowVersionRepository = businessFlowVersionRepository;
        _businessFlowBlockRepository = businessFlowBlockRepository;
    }
    
    public async Task StartAsync(int businessFlowVersionId, int submissionId)
    {
        var startBlock = await _businessFlowBlockRepository.GetStartBlockAsync(businessFlowVersionId);
        if (startBlock == null)
        {
            throw new BusinessFlowNotFoundException(businessFlowVersionId);
        }
        
        await ValidateAsync(submissionId, startBlock);
        
        var submissionExecution = new SubmissionExecution(startBlock.Id, submissionId);
        ApplyBusinessFlowBlockSetting(submissionExecution, startBlock);
        
        await _submissionExecutionRepository.InsertAsync(submissionExecution);
    }

    public async Task MoveNextAsync(int submissionId, Guid outComeId, string completedBy)
    {
        var executedSubmission = await _submissionExecutionRepository.GetExecutedAsync(submissionId, outComeId);
        if (executedSubmission != null)
        {
            throw new ExecutionHasAlreadyCompletedException(executedSubmission.Id);
        }
        
        var specification = new SubmissionExecutionSpecification(submissionId)
            .And(new SubmissionExecutionByStatusSpecification(SubmissionExecutionStatus.InProgress));
        
        var currentExecution = await _submissionExecutionRepository.FindAsync(specification);
        if (currentExecution == null)
        {
            throw new SubmissionExecutionNotFoundException(submissionId);
        }
        
        var currentBlock = await _businessFlowBlockRepository.GetBlockAsync(currentExecution.BusinessFlowBlockId);
        if (currentBlock == null)
        {
            throw new BusinessFlowBlockNotFoundException(currentExecution.BusinessFlowBlockId);
        }
        
        var isValidOutCome = currentBlock.OutComes.Any(x => x.Id == outComeId);
        if (!isValidOutCome)
        {
            throw new BusinessFlowOutComeNotFoundException(currentBlock.Id, outComeId);
        }
        
        var hasInCompleteTask = currentExecution.Tasks.Any(x => x.Status != SubmissionExecutionTaskStatus.Done);
        if (hasInCompleteTask)
        {
            throw new ExecutionInCompletedTaskException(currentExecution.Id);
        }
        
        currentExecution.MarkCompleted(outComeId, completedBy);
        
        var nextBlock = await _businessFlowBlockRepository.GetNextBlockByOutComeAsync(outComeId);
        if (nextBlock == null)
        {
            throw new SubmissionExecutionNotFoundException(submissionId);
        }
        
        var submissionExecution = new SubmissionExecution(nextBlock.Id, submissionId);
        ApplyBusinessFlowBlockSetting(submissionExecution, nextBlock);

        if (nextBlock.Type is BusinessFlowBlockType.End)
        {
            submissionExecution.MarkCompleted(null, completedBy);
        }
        
        _submissionExecutionRepository.Update(currentExecution);
        await _submissionExecutionRepository.InsertAsync(submissionExecution);
    }

    private void ApplyBusinessFlowBlockSetting(SubmissionExecution execution, BusinessFlowBlock block)
    {
        foreach (var personInCharge in block.PersonInChargeSettings)
        {
            execution.AddPersonInCharge(personInCharge.UserId);
        }
        
        foreach (var task in block.TaskSettings)
        {
            execution.AddTask(task.Name, task.Index);
        }
    }
    
    private async Task ValidateAsync(int submissionId, BusinessFlowBlock block)
    {
        var isExecuted = await _submissionExecutionRepository.AnyAsync(new SubmissionExecutionByBlockSpecification(submissionId, block.Id));
        if (isExecuted)
        {
            throw new SubmissionHasAlreadyExecutedException(submissionId, block.Id);
        }
    }
}