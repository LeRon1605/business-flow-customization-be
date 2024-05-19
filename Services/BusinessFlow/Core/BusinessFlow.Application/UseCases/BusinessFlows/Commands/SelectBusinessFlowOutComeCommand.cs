using BuildingBlocks.Application.Cqrs;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class SelectBusinessFlowOutComeCommand : ICommand
{
    public int SubmissionId { get; set; }
    
    public Guid OutComeId { get; set; }
    
    public SelectBusinessFlowOutComeCommand(int submissionId, Guid outComeId)
    {
        SubmissionId = submissionId;
        OutComeId = outComeId;
    }
}