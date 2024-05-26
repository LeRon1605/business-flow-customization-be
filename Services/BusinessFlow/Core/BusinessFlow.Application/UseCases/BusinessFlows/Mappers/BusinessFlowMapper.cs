using Application.Dtos.Notifications.Requests;
using Application.Dtos.Notifications.Responses;
using BuildingBlocks.Application.Mappers;
using BusinessFlow.Application.UseCases.BusinessFlows.Dtos;
using BusinessFlow.Domain.BusinessFlowAggregate.Models;

namespace BusinessFlow.Application.UseCases.BusinessFlows.Mappers;

public class BusinessFlowMapper : MappingProfile
{
    public BusinessFlowMapper()
    {
        CreateMap<BusinessFlowBlockRequestDto, BusinessFlowBlockModel>();
        CreateMap<BusinessFlowOutComeRequestDto, BusinessFlowOutComeModel>();
        CreateMap<BusinessFlowBranchRequestDto, BusinessFlowBranchModel>();
        CreateMap<BusinessFlowBlockTaskRequestDto, BusinessFlowBlockTaskSettingModel>();
        CreateMap<BasicBusinessFlowBlockDto, BusinessFlowNotificationDataDto>()
            .ForMember(x => x.Type, options => options.MapFrom(x => BusinessFlowNotificationDataType.BusinessFlow));
    }
}