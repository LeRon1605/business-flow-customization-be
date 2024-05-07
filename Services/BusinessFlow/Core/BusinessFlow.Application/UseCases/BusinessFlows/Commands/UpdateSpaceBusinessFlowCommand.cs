using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateSpaceBusinessFlowCommand : ICommand<int>
{
    public int Id { get; set; }
    
    public List<BusinessFlowBlockModel> Blocks { get; set; }
    
    public List<BusinessFlowBranchModel> Branches { get; set; }
    
    public UpdateSpaceBusinessFlowCommand(int id, List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        Id = id;
        Blocks = blocks;
        Branches = branches;
    }
}