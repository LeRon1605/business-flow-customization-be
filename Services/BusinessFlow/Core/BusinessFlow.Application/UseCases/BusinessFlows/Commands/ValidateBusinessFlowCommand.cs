using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class ValidateBusinessFlowCommand : ICommand<List<BusinessFlowBlockValidationModel>>
{
    public List<BusinessFlowBlockModel> Blocks { get; set; }
    
    public List<BusinessFlowBranchModel> Branches { get; set; }
    
    public ValidateBusinessFlowCommand(List<BusinessFlowBlockModel> blocks
        , List<BusinessFlowBranchModel> branches)
    {
        Blocks = blocks;
        Branches = branches;
    }
}