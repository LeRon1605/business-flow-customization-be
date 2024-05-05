using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class CreateSpaceCommand : ICommand<int>
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Color { get; set; }
    public List<BusinessFlowBlockModel> Blocks { get; set; }
    public List<BusinessFlowBranchModel> Branches { get; set; }
    
    public CreateSpaceCommand(string name, string description, string color, List<BusinessFlowBlockModel> blocks, List<BusinessFlowBranchModel> branches)
    {
        Name = name;
        Description = description;
        Color = color;
        Blocks = blocks;
        Branches = branches;
    }
}