using BusinessFlow.Domain.BusinessFlowAggregate.Enums;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBlockModel<TKey> where TKey : IEquatable<TKey>
{
    public TKey? Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowBlockType Type { get; set; }

    public List<BusinessFlowOutComeModel<TKey>> OutComes { get; set; } = new();
}