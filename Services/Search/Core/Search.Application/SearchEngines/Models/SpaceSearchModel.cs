using BuildingBlocks.Application.SearchEngines.Attributes;
using BuildingBlocks.Application.SearchEngines.Models;

namespace Search.Application.SearchEngines.Models;

[IndexName("space")]
public class SpaceSearchModel : BaseSearchModel
{
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public string? Color { get; set; }
    
    public int? TenantId { get; set; }
    
    public string? CreatedBy { get; set; }
}