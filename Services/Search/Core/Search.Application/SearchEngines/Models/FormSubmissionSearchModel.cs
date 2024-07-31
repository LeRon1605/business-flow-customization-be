using BuildingBlocks.Application.SearchEngines.Attributes;
using BuildingBlocks.Application.SearchEngines.Models;

namespace Search.Application.SearchEngines.Models;

[IndexName("form_submission")]
public class FormSubmissionSearchModel : BaseSearchModel
{
    public string Name { get; set; } = null!;
    
    public int? TenantId { get; set; }
    
    public string? CreatedBy { get; set; }
}