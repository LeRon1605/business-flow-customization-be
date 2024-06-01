using System.Linq.Expressions;
using Application.Dtos.Submissions.Responses;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class BasicSubmissionQueryDto : BasicSubmissionDto, IProjection<FormSubmission, BasicSubmissionDto>
{
    public Expression<Func<FormSubmission, BasicSubmissionDto>> GetProject()
    {
        return x => new BasicSubmissionDto()
        {
            Id = x.Id,
            Name = x.Name,
            FormVersionId = x.FormVersionId
        };
    }
}