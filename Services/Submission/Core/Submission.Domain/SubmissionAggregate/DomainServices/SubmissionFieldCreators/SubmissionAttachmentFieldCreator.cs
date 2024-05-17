using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionAttachmentFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.Attachment;

    public ISubmissionField Create(FormElement formElement, SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            if (formElement.IsRequired)
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
            
            return new SubmissionAttachmentField(field.ElementId);
        }

        try
        {
            var value = JsonConvert.DeserializeObject<SubmissionAttachmentValueModel[]>(field.Value);
            if (value == null)
            {
                throw new InvalidSubmissionFieldValueException();
            }
            
            if (formElement.IsRequired && value.Length == 0)
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
        
            return new SubmissionAttachmentField(field.ElementId, value);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}