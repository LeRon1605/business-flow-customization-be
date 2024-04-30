using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class ValidateBusinessFlowCommand : ICommand<List<BusinessFlowBlockValidationModel<string>>>
{
    public List<BusinessFlowBlockModel<string>> Blocks { get; set; }
    
    public List<BusinessFlowBranchModel<string>> Branches { get; set; }
    
    public ValidateBusinessFlowCommand(List<BusinessFlowBlockModel<string>> blocks
        , List<BusinessFlowBranchModel<string>> branches)
    {
        Blocks = blocks;
        Branches = branches;
    }
}