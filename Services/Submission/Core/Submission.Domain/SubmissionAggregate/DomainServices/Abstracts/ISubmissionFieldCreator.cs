using Submission.Domain.FormElementAggregate.Enums;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionFieldCreator
{
    FormElementType Type { get; set; }
    
    ISubmissionField Create(SubmissionFieldModel field);
}