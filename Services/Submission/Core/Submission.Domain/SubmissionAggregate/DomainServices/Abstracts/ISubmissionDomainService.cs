using BuildingBlocks.Domain.Services;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Models;

namespace Submission.Domain.SubmissionAggregate.DomainServices.Abstracts;

public interface ISubmissionDomainService : IDomainService
{
    Task<FormSubmission> CreateAsync(int spaceId, SubmissionModel submission);
}