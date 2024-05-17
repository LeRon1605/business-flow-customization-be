using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionSingleOptionFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.SingleOption;

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
            var optionId = JsonConvert.DeserializeObject<int>(field.Value);
            return new SubmissionOptionField(field.ElementId, new []{ optionId });
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}