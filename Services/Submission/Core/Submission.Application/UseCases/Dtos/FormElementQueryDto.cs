using System.Linq.Expressions;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Application.UseCases.Dtos;

public class FormElementQueryDto : FormElementDto, IProjection<FormElement, FormElementQueryDto>
{
    public Guid? BusinessFlowBlockId { get; set; }
    public Expression<Func<FormElement, FormElementQueryDto>> GetProject()
    {
        return e => new FormElementQueryDto()
        {
            Id = e.Id,
            BusinessFlowBlockId = e.FormVersion.Form.BusinessFlowBlockId,
            Name = e.Name,
            Description = e.Description,
            Type = e.Type,
            Index = e.Index,
            Settings = e.Settings.Select(s => new FormElementSettingDto()
            {
                Id = s.Id,
                Type = s.Type,
                Value = s.Value
            }).ToList(),
            Options = e.Options.Select(o => new OptionFormElementSettingDto()
            {
                Id = o.Id,
                Name = o.Name
            }).ToList()
        };
    }
}