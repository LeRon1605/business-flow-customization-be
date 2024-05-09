using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class FormSubmissionRepository : EfCoreRepository<FormSubmission>, IFormSubmissionRepository
{
    public FormSubmissionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}