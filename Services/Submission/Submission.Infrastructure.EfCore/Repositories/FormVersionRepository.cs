using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class FormVersionRepository : EfCoreRepository<FormVersion>, IFormVersionRepository
{
    public FormVersionRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public Task<TOut?> GetLatestSpaceVersionAsync<TOut>(int spaceId, IProjection<FormVersion, TOut> projection)
    {
        return GetQueryable()
            .Where(x => x.Form.SpaceId == spaceId && x.Form.BusinessFlowBlockId == null)
            .OrderByDescending(x => x.Id)
            .Select(projection.GetProject())
            .FirstOrDefaultAsync();
    }

    public Task<TOut?> GetAsync<TOut>(int spaceId, int versionId, IProjection<FormVersion, TOut> projection)
    {
        return GetQueryable()
            .Where(x => x.Form.SpaceId == spaceId && x.Id == versionId)
            .Select(projection.GetProject())
            .FirstOrDefaultAsync();
    }

    public Task<TOut?> GetAsync<TOut>(Guid businessFlowBlockId, IProjection<FormVersion, TOut> projection)
    {
        return GetQueryable()
            .Where(x => x.Form.BusinessFlowBlockId == businessFlowBlockId)
            .Select(projection.GetProject())
            .FirstOrDefaultAsync();
    }

    public Task<List<TOut>> GetBySpaceAsync<TOut>(int spaceId, IProjection<FormVersion, TOut> projection)
    {
        return GetQueryable()
            .OrderByDescending(x => x.Id)
            .Where(x => x.Form.SpaceId == spaceId && x.Form.BusinessFlowBlockId == null)
            .Select(projection.GetProject())
            .ToListAsync();
    }

    public Task<TOut?> GetLatestVersionByTokenAsync<TOut>(string token, IProjection<FormVersion, TOut> projection)
    {
        return DbSet
            .Where(x => x.Form.PublicToken == token)
            .OrderByDescending(x => x.Id)
            .Select(projection.GetProject())
            .FirstOrDefaultAsync();
    }
}