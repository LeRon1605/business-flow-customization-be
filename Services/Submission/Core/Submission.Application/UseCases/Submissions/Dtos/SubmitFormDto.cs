using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

public class ExternalSubmitFormDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Token { get; set; } = null!;
    
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
        Value = JsonConvert.SerializeObject(model.Value, new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        });
    }
    
    public SubmissionFieldDto(SubmissionAttachmentFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.Attachments, new JsonSerializerSettings 
        { 
            ContractResolver = new CamelCasePropertyNamesContractResolver() 
        });
    }
    
    public SubmissionFieldDto(SubmissionOptionFieldModel model)
    {
        ElementId = model.ElementId;
        Value = JsonConvert.SerializeObject(model.OptionIds);
    }
}