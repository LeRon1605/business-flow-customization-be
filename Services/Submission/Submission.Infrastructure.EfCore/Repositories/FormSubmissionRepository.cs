using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.SubmissionAggregate.Entities;
using Submission.Domain.SubmissionAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class FormSubmissionRepository : EfCoreRepository<FormSubmission>, IFormSubmissionRepository
{
    public FormSubmissionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public Task<TOut?> FindByTrackingTokenAsync<TOut>(string trackingToken, IProjection<FormSubmission, TOut> projection)
    {
        return DbSet
            .Where(_ => _.TrackingToken == trackingToken)
            .Select(projection.GetProject().Expand())
            .FirstOrDefaultAsync();
    }
}