using System.ComponentModel.DataAnnotations;
using Application.Dtos.Submissions.Requests;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class BusinessFlowBlockRequestDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowBlockType Type { get; set; }

    public List<BusinessFlowOutComeRequestDto> OutComes { get; set; } = new();

    public List<FormElementRequestDto> Elements { get; set; } = new();
    
    public List<string> PersonInChargeIds { get; set; } = new();
    
    public List<BusinessFlowBlockTaskRequestDto> Tasks { get; set; } = new();
}

public class BusinessFlowOutComeRequestDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;
}

public class BusinessFlowBranchRequestDto
{
    public Guid FromBlockId { get; set; }
    
    public Guid ToBlockId { get; set; }
    
    public Guid? OutComeId { get; set; }
}

public class BusinessFlowBlockTaskRequestDto
{
    [Required] 
    public string Name { get; set; } = null!;
    
    public double Index { get; set; }
}