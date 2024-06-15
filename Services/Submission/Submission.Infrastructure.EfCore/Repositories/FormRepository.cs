using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Submission.Domain.FormAggregate.Entities;
using Submission.Domain.FormAggregate.Repositories;

namespace Submission.Infrastructure.EfCore.Repositories;

public class FormRepository : EfCoreRepository<Form>, IFormRepository
{
    public FormRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
    }

    public Task<Form?> FindByBusinessFlowBlockIdAsync(Guid businessFlowBlockId)
    {
        return GetQueryable()
            .Include(x => x.Versions)
            .FirstOrDefaultAsync(x => x.BusinessFlowBlockId.HasValue && x.BusinessFlowBlockId == businessFlowBlockId);
    }

    public Task<List<TOut>> GetBySpacesAsync<TOut>(List<int> spaceIds, IProjection<FormVersion, TOut> projection)
    {
        return GetQueryable()
            .Where(x => spaceIds.Contains(x.SpaceId) && x.BusinessFlowBlockId == null)
            .SelectMany(x => x.Versions
                .OrderByDescending(v => v.Id)
                .Take(1))
            .Select(projection.GetProject())
            .ToListAsync();
    }

    public Task<Form?> FindBySpaceIdAsync(int spaceId)
    {
        return GetQueryable()
            .Include(x => x.Versions)
            .FirstOrDefaultAsync(x => x.SpaceId == spaceId);
    }

    public Task<Form?> FindByPublicTokenAsync(string token)
    {
        return GetQueryable()
            .Include(x => x.Versions)
            .FirstOrDefaultAsync(x => !x.PublicToken.IsNullOrEmpty() && x.PublicToken == token);
    }
}