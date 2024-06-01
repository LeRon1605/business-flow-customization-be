using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using Application.Dtos.Spaces;
using BuildingBlocks.Application.Mappers;
using BusinessFlow.Application.UseCases.Spaces.Dtos;

namespace BusinessFlow.Application.UseCases.Spaces.Mappers;

public class SpaceMapper : MappingProfile
{
    public SpaceMapper()
    {
        CreateMap<SpaceDto, BusinessFlowNotificationDataDto>()
            .ForMember(x => x.Type, options => options.MapFrom(x => BusinessFlowNotificationDataType.Space));
    }
}