using BusinessFlow.Domain.BusinessFlowAggregate.Enums;

namespace BusinessFlow.Domain.BusinessFlowAggregate.Models;

public class BusinessFlowBlockModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public BusinessFlowBlockType Type { get; set; }

    public List<BusinessFlowOutComeModel> OutComes { get; set; } = new();
    
    public List<BusinessFlowBlockTaskSettingModel> Tasks { get; set; } = new();
    
    public List<string> PersonInChargeIds { get; set; } = new();
}