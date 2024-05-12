using Domain.Enums;
using Newtonsoft.Json;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Exceptions;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.SubmissionFieldCreators;

public class SubmissionAttachmentFieldCreator : ISubmissionFieldCreator
{
    public FormElementType Type { get; set; } = FormElementType.Attachment;

    public ISubmissionField Create(SubmissionFieldModel field)
    {
        if (field.Value == null)
        {
            return new SubmissionAttachmentField(field.ElementId);
        }

        try
        {
            var value = JsonConvert.DeserializeObject<SubmissionAttachmentValueModel[]>(field.Value);
            if (value == null)
            {
                throw new InvalidSubmissionFieldValueException();
            }
        
            return new SubmissionAttachmentField(field.ElementId, value);
        }
        catch
        {
            throw new InvalidSubmissionFieldValueException();
        }
    }
}