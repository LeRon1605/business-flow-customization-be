using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class BasicSubmissionDto : IProjection<FormSubmission, BasicSubmissionDto>
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public Expression<Func<FormSubmission, BasicSubmissionDto>> GetProject()
    {
        return x => new BasicSubmissionDto()
        {
            Id = x.Id,
            Name = x.Name
        };
    }
}