using Newtonsoft.Json;
using Submission.Domain.FormAggregate.Enums;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionNumberFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.Number;

    public ISubmissionField Create(SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            return new SubmissionNumberValue(field.ElementId);
        }

        try
        {
            var value = JsonConvert.DeserializeObject<decimal>(field.Value);
            return new SubmissionNumberValue(field.ElementId, value);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}