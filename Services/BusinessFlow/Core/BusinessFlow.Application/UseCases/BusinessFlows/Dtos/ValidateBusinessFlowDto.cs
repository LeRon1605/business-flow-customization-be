using System.ComponentModel.DataAnnotations;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class ValidateBusinessFlowDto
{
    [Required] 
    public List<BusinessFlowBlockModel<string>> Blocks { get; set; } = null!;

    [Required] 
    public List<BusinessFlowBranchModel<string>> Branches { get; set; } = null!;
}