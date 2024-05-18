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
    }
}