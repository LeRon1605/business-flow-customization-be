using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Entities;
using BusinessFlow.Domain.SubmissionExecutionAggregate.Enums;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class SubmissionExecutionBusinessFlowDto : IProjection<SubmissionExecution, SubmissionExecutionBusinessFlowDto>
{
    public int Id { get; set; }
    
    public SubmissionExecutionStatus Status { get; set; }
    
    public string? CompletedBy { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public BusinessFlowOutComeDto? OutCome { get; set; }
    
    public SubmissionExecutionBusinessFlowBlockDto BusinessFlowBlock { get; set; } = null!;
    
    public List<SubmissionExecutionTaskDto> Tasks { get; set; } = new();
    
    public List<string> PersonInChargeIds { get; set; } = new();
    
    public Expression<Func<SubmissionExecution, SubmissionExecutionBusinessFlowDto>> GetProject()
    {
        return x => new SubmissionExecutionBusinessFlowDto()
        {
            Id = x.Id,
            Status = x.Status,
            CompletedBy = x.CompletedBy,
            CompletedAt = x.CompletedAt,
            OutCome = x.OutCome != null 
                ? new BusinessFlowOutComeDto() 
                {
                    Id = x.OutCome.Id,
                    Name = x.OutCome.Name,
                    Color = x.OutCome.Color
                } 
                : null,
            PersonInChargeIds = x.PersonInCharges.Select(p => p.UserId).ToList(),
            Tasks = x.Tasks.OrderBy(t => t.Index).Select(t => new SubmissionExecutionTaskDto
            {
                Id = t.Id,
                Status = t.Status,
                Name = t.Name,
                Index = t.Index
            }).ToList(),
            BusinessFlowBlock = new SubmissionExecutionBusinessFlowBlockDto()
            {
                Id = x.BusinessFlowBlock.Id,
                Name = x.BusinessFlowBlock.Name,
                Type = x.BusinessFlowBlock.Type,
                FormId = x.BusinessFlowBlock.FormId,
                OutComes = x.BusinessFlowBlock.OutComes.Select(o => new SubmissionExecutionOutComeDto()
                {
                    OutCome = new BusinessFlowOutComeDto()
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Color = o.Color
                    },
                    ToBlock = o.Branches
                        .Where(b => b.FromBlockId == x.BusinessFlowBlock.Id)
                        .Select(b => new BasicBusinessFlowBlockDto()
                        {
                            Id = b.ToBlock.Id,
                            Name = b.ToBlock.Name,
                            Type = b.ToBlock.Type
                        })
                        .FirstOrDefault()
                }).ToList()
            }
        };
    }
}

public class SubmissionExecutionBusinessFlowBlockDto : BasicBusinessFlowBlockDto
{
    public int? FormId { get; set; }
    
    public List<SubmissionExecutionOutComeDto> OutComes { get; set; } = new();
}

public class SubmissionExecutionOutComeDto
{
    public BusinessFlowOutComeDto OutCome { get; set; } = null!;
    
    public BasicBusinessFlowBlockDto? ToBlock { get; set; } = null!;
}

public class SubmissionExecutionTaskDto
{
    public int Id { get; set; }
    
    public SubmissionExecutionTaskStatus Status { get; set; }
    
    public string Name { get; set; } = null!;
    
    public double Index { get; set; }
}