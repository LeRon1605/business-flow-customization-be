using Domain.Enums;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Abstracts;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionFieldCreator
{
    FormElementType Type { get; set; }
    
    ISubmissionField Create(FormElement formElement, SubmissionFieldModel field);
}