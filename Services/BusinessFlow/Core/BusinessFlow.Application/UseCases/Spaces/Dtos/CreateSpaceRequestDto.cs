using System.ComponentModel.DataAnnotations;
using Application.Dtos.Submissions.Requests;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Dtos;

public class CreateSpaceRequestDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;
    
    [Required]
    public string Color { get; set; } = null!;
    
    [Required]
    public CreateBusinessFlowRequestDto BusinessFlow { get; set; } = null!;
    
    [Required]
    public CreateFormRequestDto Form { get; set; } = null!;
}