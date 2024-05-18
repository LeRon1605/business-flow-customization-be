using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Cqrs;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.Spaces.Commands;

public class CreateSpaceCommand : ICommand<int>
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Color { get; set; }
    public List<BusinessFlowBlockRequestDto> Blocks { get; set; }
    public List<BusinessFlowBranchRequestDto> Branches { get; set; }
    public FormRequestDto Form { get; set; }
    
    public CreateSpaceCommand(string name
        , string description
        , string color
        , List<BusinessFlowBlockRequestDto> blocks
        , List<BusinessFlowBranchRequestDto> branches
        , FormRequestDto form)
    {
        Name = name;
        Description = description;
        Color = color;
        Blocks = blocks;
        Branches = branches;
        Form = form;
    }
}