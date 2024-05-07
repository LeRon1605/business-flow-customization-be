using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using BusinessFlow.Domain.BusinessFlowAggregate.Entities;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Dtos;

public class BusinessFlowVersionDto : IProjection<BusinessFlowVersion, BusinessFlowVersionDto>
{
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Expression<Func<BusinessFlowVersion, BusinessFlowVersionDto>> GetProject()
    {
        return x => new BusinessFlowVersionDto()
        {
            Id = x.Id,
            CreatedAt = x.Created
        };
    }
}