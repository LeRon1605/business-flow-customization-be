using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Application.UseCases.Dtos;

public class FormDto : IProjection<FormVersion, FormDto>
{
    public int Id { get; set; }
    
    public int VersionId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string CoverImageUrl { get; set; } = null!;

    public List<FormElementDto> Elements { get; set; } = null!;

    public Expression<Func<FormVersion, FormDto>> GetProject()
    {
        return x => new FormDto()
        {
            Id = x.Form.Id,
            VersionId = x.Id,
            Name = x.Form.Name,
            CoverImageUrl = x.Form.CoverImageUrl,
            Elements = x.Elements.Select(e => new FormElementDto()
            {
                Id = e.Id,
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
            }).ToList()
        };
    }
}