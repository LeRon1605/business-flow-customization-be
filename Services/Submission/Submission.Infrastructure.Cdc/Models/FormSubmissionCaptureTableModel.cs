using BuildingBlocks.EventBus;
using BuildingBlocks.EventBus.Enums;
using BuildingBlocks.Infrastructure.Cdc.Attributes;
using BuildingBlocks.Infrastructure.Cdc.Models;
using IntegrationEvents.Cdc;

namespace Submission.Infrastructure.Cdc.Models;

[CdcTableCaptureName("dbo_FormSubmission")]
public class FormSubmissionCaptureTableModel : ICaptureTableModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int TenantId { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public EntityTrackingIntegrationEvent GetIntegrationEvent(EntityAction action)
    {
        return new FormSubmissionEntityTrackingIntegrationEvent(Id, Name, CreatedBy, TenantId, action);
    }
}