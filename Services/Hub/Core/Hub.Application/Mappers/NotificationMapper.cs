using BuildingBlocks.Application.Mappers;
using Hub.Domain.NotificationAggregate.Entities;
using Hub.Domain.NotificationAggregate.Models;

namespace Hub.Application.Mappers;

public class NotificationMapper : MappingProfile
{
    public NotificationMapper()
    {
        CreateMap<Notification, NotificationModel>();
    }
}