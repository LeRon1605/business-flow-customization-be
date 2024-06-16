using System.Linq.Expressions;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using Newtonsoft.Json;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class BusinessFlowDto : IProjection<BusinessFlowVersion, int, BusinessFlowDto>
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public List<BusinessFlowBlockDto> Blocks { get; set; } = null!;

    public List<BusinessFlowBranchDto> Branches => Blocks.SelectMany(b => b.Branches).ToList();
    
    public Expression<Func<BusinessFlowVersion, BusinessFlowDto>> GetProject()
    {
        return x => new BusinessFlowDto()
        {
            Id = x.Id,
            CreatedAt = x.Created,
            Blocks = x.Blocks.Select(b => new BusinessFlowBlockDto()
            {
                Id = b.Id,
                Name = b.Name,
                Type = b.Type,
                Tasks = b.TaskSettings
                    .OrderBy(t => t.Id)
                    .Select(t => new BusinessFlowBlockTaskDto()
                    {
                        Name = t.Name,
                        Index = t.Index
                    }).ToList(),
                PersonInChargeIds = b.PersonInChargeSettings.Select(p => p.UserId).ToList(),
                Branches = b.FromBranches.Select(br => new BusinessFlowBranchDto()
                {
                    Id = br.Id,
                    FromBlockId = br.FromBlockId,
                    ToBlockId = br.ToBlockId,
                    OutCome = br.OutCome != null 
                        ? new BusinessFlowOutComeDto() 
                        {
                            Id = br.OutCome.Id,
                            Name = br.OutCome.Name,
                            Color = br.OutCome.Color
                        } 
                        : null
                }).ToList(),
                OutComes = b.OutComes.Select(o => new BusinessFlowOutComeDto()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Color = o.Color
                }).ToList(),
            }).ToList(),
        };
    }
}

public class BasicBusinessFlowBlockDto : IProjection<BusinessFlowBlock, Guid, BasicBusinessFlowBlockDto>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowBlockType Type { get; set; }
    
    public Expression<Func<BusinessFlowBlock, BasicBusinessFlowBlockDto>> GetProject()
    {
        return x => new BasicBusinessFlowBlockDto()
        {
            Id = x.Id,
            Name = x.Name,
            Type = x.Type
        };
    }
}

public class BusinessFlowBlockDto : BasicBusinessFlowBlockDto
{
    
    [JsonIgnore]
    public List<BusinessFlowBranchDto> Branches { get; set; } = new();

    public List<FormElementDto> Elements { get; set; } = new();
    
    public List<BusinessFlowBlockTaskDto> Tasks { get; set; } = new();
    
    public List<string> PersonInChargeIds { get; set; } = new();
    
    public List<BusinessFlowOutComeDto> OutComes { get; set; } = new();
}

public class BusinessFlowBranchDto
{
    public int Id { get; set; }
    
    public Guid FromBlockId { get; set; }
    
    public Guid ToBlockId { get; set; }
    
    public BusinessFlowOutComeDto? OutCome { get; set; }
}

public class BusinessFlowOutComeDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;
}

public class BusinessFlowBlockTaskDto
{
    public string Name { get; set; } = null!;
    
    public double Index { get; set; }
}