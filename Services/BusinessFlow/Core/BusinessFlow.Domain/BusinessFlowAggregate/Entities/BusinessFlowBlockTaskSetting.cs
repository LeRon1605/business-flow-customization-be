using BuildingBlocks.Domain.Models;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Entities;

public class BusinessFlowBlockTaskSetting : Entity
{
    public string Name { get; private set; }
    
    public double Index { get; private set; }
    
    public Guid BusinessFlowBlockId { get; private set; }
    
    public virtual BusinessFlowBlock BusinessFlowBlock { get; private set; } = null!;
    
    public BusinessFlowBlockTaskSetting(string name, double index)
    {
        Name = name;
        Index = index;
    }
    
    public BusinessFlowBlockTaskSetting()
    {
    }
}