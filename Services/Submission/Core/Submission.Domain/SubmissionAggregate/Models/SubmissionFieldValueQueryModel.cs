using System.Linq.Expressions;
using BuildingBlocks.Domain.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;

namespace Submission.Domain.SubmissionAggregate.Models;

public class SubmissionFieldValueQueryModel : IProjection<FormSubmission, SubmissionFieldValueQueryModel>
{
    public List<SubmissionNumberFieldModel> NumberFields { get; set; } = new();
    
    public List<SubmissionTextFieldModel> TextFields { get; set; } = new();
    
    public List<SubmissionDateFieldModel> DateFields { get; set; } = new();
    
    public List<SubmissionOptionFieldModel> OptionFields { get; set; } = new();
    
    public List<SubmissionAttachmentFieldModel> AttachmentFields { get; set; } = new();
    
    public Expression<Func<FormSubmission, SubmissionFieldValueQueryModel>> GetProject()
    {
        return x => new SubmissionFieldValueQueryModel()
        {
            NumberFields = x.NumberFields.Select(n => new SubmissionNumberFieldModel()
            {
                ElementId = n.ElementId,
                Value = n.Value
            }).ToList(),
            
            TextFields = x.TextFields.Select(t => new SubmissionTextFieldModel()
            {
                ElementId = t.ElementId,
                Value = t.Value
            }).ToList(),
            
            DateFields = x.DateFields.Select(d => new SubmissionDateFieldModel()
            {
                ElementId = d.ElementId,
                Value = d.Value
            }).ToList(),
            
            OptionFields = x.OptionFields.Select(o => new SubmissionOptionFieldModel()
            {
                ElementId = o.ElementId,
                OptionIds = o.Values.Select(v => v.OptionId).ToArray()
            }).ToList(),
            
            AttachmentFields = x.AttachmentFields.Select(a => new SubmissionAttachmentFieldModel()
            {
                ElementId = a.ElementId,
                Attachments = a.Values.Select(v => new SubmissionAttachmentValueModel
                {
                    Name = v.FileName,
                    FileUrl = v.FileUrl,
                }).ToArray()
            }).ToList()
        };
    }
}