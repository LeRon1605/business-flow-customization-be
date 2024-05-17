using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Application.UseCases.Submissions.Dtos;

public class SubmitFormDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public int FormVersionId { get; set; }
    
    [Required]
    public List<SubmissionFieldDto> Fields { get; set; } = null!;
}

public class SubmissionFieldDto
{
    [Required]
    public int ElementId { get; set; }

    public string? Value { get; set; }

    public SubmissionFieldDto() 
    {
    
    }
    
    public SubmissionFieldDto(SubmissionNumberFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.Value);
    }
    
    public SubmissionFieldDto(SubmissionTextFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.Value);
    }
    
    public SubmissionFieldDto(SubmissionDateFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.Value);
    }
    
    public SubmissionFieldDto(SubmissionAttachmentFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.Attachments);
    }
    
    public SubmissionFieldDto(SubmissionOptionFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.OptionIds);
    }
}