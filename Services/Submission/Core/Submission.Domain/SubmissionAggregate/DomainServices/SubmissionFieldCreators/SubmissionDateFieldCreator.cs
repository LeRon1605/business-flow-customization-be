using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionDateFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.Date;

    public ISubmissionField Create(SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            return new SubmissionDateValue(field.ElementId);
        }

        try
        {
            var value = JsonConvert.DeserializeObject<DateTime>(field.Value);
            return new SubmissionDateValue(field.ElementId, value);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}