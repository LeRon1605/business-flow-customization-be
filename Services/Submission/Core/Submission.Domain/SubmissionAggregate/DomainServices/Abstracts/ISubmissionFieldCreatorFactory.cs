using BuildingBlocks.Domain.Services;
using Domain.Enums;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionFieldCreatorFactory : IDomainService
{
    ISubmissionFieldCreator Get(FormElementType type);
}