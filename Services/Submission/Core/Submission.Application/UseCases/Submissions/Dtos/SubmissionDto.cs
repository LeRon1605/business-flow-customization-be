using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using LinqKit;
using Newtonsoft.Json;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmissionDto : IProjection<FormSubmission, SubmissionDto>
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public string? UpdatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    [JsonIgnore]
    public SubmissionFieldValueQueryModel FieldValues { get; set; } = null!;
    
    public List<SubmissionFieldDto> Fields => 
        FieldValues.NumberFields
            .Select(n => new SubmissionFieldDto(n))
            .Concat(FieldValues.TextFields.Select(t => new SubmissionFieldDto(t)))
            .Concat(FieldValues.DateFields.Select(d => new SubmissionFieldDto(d)))
            .Concat(FieldValues.OptionFields.Select(o => new SubmissionFieldDto(o)))
            .Concat(FieldValues.AttachmentFields.Select(a => new SubmissionFieldDto(a)))
            .ToList();
    
    public Expression<Func<FormSubmission, SubmissionDto>> GetProject()
    {
        return x => new SubmissionDto()
        {
            Id = x.Id,
            Name = x.Name,
            CreatedBy = x.CreatedBy!,
            CreatedAt = x.Created,
            UpdatedBy = x.LastModifiedBy,
            UpdatedAt = x.LastModified,
            FieldValues = new SubmissionFieldValueQueryModel().GetProject().Invoke(x)
        };
    }
}