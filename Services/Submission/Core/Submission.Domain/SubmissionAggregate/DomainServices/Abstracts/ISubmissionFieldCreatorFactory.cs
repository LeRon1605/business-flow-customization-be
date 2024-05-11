using BuildingBlocks.Domain.Services;
using Submission.Domain.FormElementAggregate.Enums;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionFieldCreatorFactory : IDomainService
{
    ISubmissionFieldCreator Get(FormElementType type);
}