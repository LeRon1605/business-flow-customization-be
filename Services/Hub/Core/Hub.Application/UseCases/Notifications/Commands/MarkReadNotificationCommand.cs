using BuildingBlocks.Application.Cqrs;

namespace Hub.Application.UseCases.Notifications.Commands;

public class MarkReadNotificationCommand : ICommand
{
    public Guid Id { get; set; }
    
    public MarkReadNotificationCommand(Guid id)
    {
        Id = id;
    }
}