using BuildingBlocks.Application.Mappers;
using Hub.Application.Dtos;
using Hub.Domain.NotificationAggregate.Entities;

namespace Hub.Application.UseCases.Notifications.Mappers;

public class NotificationMapper : MappingProfile
{
    public NotificationMapper()
    {
        CreateMap<Notification, NotificationDto>();
    }
}