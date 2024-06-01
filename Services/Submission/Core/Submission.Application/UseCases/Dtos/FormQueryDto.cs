using System.Linq.Expressions;
using Application.Dtos.Forms;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.FormAggregate.Entities;

namespace Submission.Application.UseCases.Dtos;

public class FormQueryDto : IProjection<FormVersion, BasicFormDto>
{
    public Expression<Func<FormVersion, BasicFormDto>> GetProject()
    {
        return x => new BasicFormDto()
        {
            Id = x.FormId,
            VersionId = x.Id,
            Name = x.Form.Name,
            SpaceId = x.Form.SpaceId
        };
    }
}