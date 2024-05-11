using BuildingBlocks.Domain.Services;
using Submission.Domain.FormAggregate.Enums;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionFieldCreatorFactory : IDomainService
{
    ISubmissionFieldCreator Get(FormElementType type);
}