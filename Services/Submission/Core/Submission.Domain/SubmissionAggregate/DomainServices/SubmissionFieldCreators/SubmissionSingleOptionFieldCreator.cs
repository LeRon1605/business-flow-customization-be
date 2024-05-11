using Newtonsoft.Json;
using Submission.Domain.FormElementAggregate.Enums;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionSingleOptionFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.SingleOption;

    public ISubmissionField Create(SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            return new SubmissionOptionField(field.ElementId);
        }

        try
        {
            var optionIds = JsonConvert.DeserializeObject<int[]>(field.Value);
            if (optionIds == null || optionIds.Length > 1)
            {
                throw new InvalidSubmissionFieldValueException();
            }
        
            return new SubmissionOptionField(field.ElementId, optionIds);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}