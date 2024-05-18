using Application.Dtos.Submissions.Requests;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Application.Mappers;
using Submission.Application.UseCases.Dtos;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Application.UseCases.Forms.Mappers;

public class FormMapper : MappingProfile
{
    public FormMapper()
    {
        CreateMap<FormRequestDto, FormModel>();
        CreateMap<FormElementRequestDto, FormElementModel>();
        CreateMap<FormElementSettingRequestDto, FormElementSettingModel>();
        CreateMap<OptionFormElementSettingRequestDto, OptionFormElementSettingModel>();
        CreateMap<FormElementQueryDto, FormElementDto>();
    }
}