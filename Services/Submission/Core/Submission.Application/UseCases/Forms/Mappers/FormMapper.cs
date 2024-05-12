using Application.Dtos.Submissions.Requests;
using BuildingBlocks.Application.Mappers;
using Submission.Domain.FormAggregate.Models;

namespace Submission.Application.UseCases.Forms.Mappers;

public class FormMapper : MappingProfile
{
    public FormMapper()
    {
        CreateMap<CreateFormRequestDto, FormModel>();
        CreateMap<FormElementRequestDto, FormElementModel>();
        CreateMap<FormElementSettingDto, FormElementSettingModel>();
        CreateMap<OptionFormElementSettingDto, OptionFormElementSettingModel>();
    }
}