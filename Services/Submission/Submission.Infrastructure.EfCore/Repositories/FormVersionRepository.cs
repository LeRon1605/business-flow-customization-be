using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class FormVersionRepository : EfCoreRepository<FormVersion>, IFormVersionRepository
{
    public FormVersionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }
}