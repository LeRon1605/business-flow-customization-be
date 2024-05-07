using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;
using BusinessFlow.Domain.BusinessFlowAggregate.Enums;
using Newtonsoft.Json;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class BusinessFlowDto : IProjection<BusinessFlowVersion, BusinessFlowDto>
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

public class BusinessFlowBlockDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowBlockType Type { get; set; }
    
    [JsonIgnore]
    public List<BusinessFlowBranchDto> Branches { get; set; } = new();

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