using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionTextFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.Text;

    public ISubmissionField Create(FormElement formElement, SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            if (formElement.IsRequired)
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
            
            return new SubmissionTextValue(field.ElementId);
        }

        try
        {
            var value = JsonConvert.DeserializeObject<string>(field.Value);
            if (value == null)
            {
                throw new InvalidSubmissionFieldValueException();
            }
            
            if (formElement.IsRequired && string.IsNullOrWhiteSpace(value))
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
        
            return new SubmissionTextValue(field.ElementId, value);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}