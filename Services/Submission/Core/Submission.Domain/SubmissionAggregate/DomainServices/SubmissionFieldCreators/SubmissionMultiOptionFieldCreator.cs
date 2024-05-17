using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionMultiOptionFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.MultiOption;
    
    public ISubmissionField Create(FormElement formElement, SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            if (formElement.IsRequired)
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
            
            return new SubmissionOptionField(field.ElementId);
        }

        try
        {
            var optionIds = JsonConvert.DeserializeObject<int[]>(field.Value);
            if (optionIds == null)
            {
                throw new InvalidSubmissionFieldValueException();
            }
            
            if (formElement.IsRequired && optionIds.Length == 0)
            {
                throw new SubmissionFieldValueIsRequiredException(formElement.Id);
            }
        
            return new SubmissionOptionField(field.ElementId, optionIds);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}