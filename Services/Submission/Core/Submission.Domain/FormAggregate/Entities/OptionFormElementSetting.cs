using BuildingBlocks.Domain.Models;

namespace Submission.Domain.FormAggregate.Entities;

public class OptionFormElementSetting : Entity
{
    public string Name { get; private set; }
    
    public int FormElementId { get; private set; }
    
    public virtual FormElement FormElement { get; private set; } = null!;
    
    public OptionFormElementSetting(string name)
    {
        Name = name;
    }
    
    private OptionFormElementSetting()
    {
    }
}