using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Mappers;
using BusinessFlow.Application.UseCases.Notifications.Dtos;

namespace BusinessFlow.Application.UseCases.Notifications.Mappers;

public class BusinessFlowNotificationMapper : MappingProfile
{
    public BusinessFlowNotificationMapper()
    {
        CreateMap<PersonInChargeQueryDto, BusinessFlowNotificationDataDto>();
    }
}