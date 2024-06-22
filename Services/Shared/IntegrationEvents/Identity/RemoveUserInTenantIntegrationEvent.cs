using BuildingBlocks.EventBus;

namespace IntegrationEvents.Identity;

public class RemoveUserInTenantIntegrationEvent : IntegrationEvent
{
    public string DeletedUserId { get; set; }
    
    public RemoveUserInTenantIntegrationEvent(string deletedUserId)
    {
        DeletedUserId = deletedUserId;
    }
}