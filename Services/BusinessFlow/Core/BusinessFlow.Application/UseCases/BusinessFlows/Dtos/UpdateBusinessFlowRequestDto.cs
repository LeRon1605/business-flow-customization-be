using System.ComponentModel.DataAnnotations;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class UpdateBusinessFlowRequestDto
{
    [Required] 
    public List<BusinessFlowBlockRequestDto> Blocks { get; set; } = null!;

    [Required] 
    public List<BusinessFlowBranchRequestDto> Branches { get; set; } = null!;
}