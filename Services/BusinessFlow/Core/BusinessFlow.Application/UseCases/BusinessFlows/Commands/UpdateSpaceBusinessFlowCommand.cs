using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Commands;

public class UpdateSpaceBusinessFlowCommand : ICommand<int>
{
    public int Id { get; set; }
    
    public List<BusinessFlowBlockRequestDto> Blocks { get; set; }
    
    public List<BusinessFlowBranchRequestDto> Branches { get; set; }
    
    public UpdateSpaceBusinessFlowCommand(int id, List<BusinessFlowBlockRequestDto> blocks, List<BusinessFlowBranchRequestDto> branches)
    {
        Id = id;
        Blocks = blocks;
        Branches = branches;
    }
}