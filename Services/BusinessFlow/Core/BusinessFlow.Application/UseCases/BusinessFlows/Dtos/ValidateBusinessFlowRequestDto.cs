using System.ComponentModel.DataAnnotations;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class ValidateBusinessFlowRequestDto
{
    [Required] 
    public List<BusinessFlowBlockModel> Blocks { get; set; } = null!;

    [Required] 
    public List<BusinessFlowBranchModel> Branches { get; set; } = null!;
}