using System.ComponentModel.DataAnnotations;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class SelectBusinessFlowOutComeRequestDto
{
    [Required]
    public Guid OutComeId { get; set; }
}