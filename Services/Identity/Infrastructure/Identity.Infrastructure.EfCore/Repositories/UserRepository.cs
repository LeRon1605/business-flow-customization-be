﻿using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Identity.Domain.RoleAggregate.Entities;
using Identity.Domain.UserAggregate;
using Identity.Domain.UserAggregate.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.EfCore.Repositories;

public class UserRepository : EfCoreRepository<ApplicationUser, string>, IUserRepository
{
    public UserRepository(DbContextFactory dbContextFactory, ICurrentUser currentUser) : base(dbContextFactory, currentUser)
    {
        AddInclude(x => x.RefreshToken);
        AddInclude(x => x.Tenants);
        AddInclude(x => x.Roles);
        AddInclude("Roles.Role");
    }

    public Task<ApplicationUser?> FindByUserNameOrEmailAsync(string username, string email)
    {
        return GetQueryable().FirstOrDefaultAsync(x => x.UserName == username || x.Email == email);
    }

    public Task<ApplicationUser?> FindByUserNameOrEmailAndRoles(string username, string email, IEnumerable<string> roles)
    {
        var rolesQueryable = DbContext.Set<ApplicationRole>().Where(x => roles.Any(r => r == x.Name));
        var userRolesQueryable = DbContext.Set<ApplicationUserInRole>().Where(x => rolesQueryable.Any(r => r.Id == x.RoleId));

        return GetQueryable().FirstOrDefaultAsync(x => (x.UserName == username || x.Email == email) && userRolesQueryable.Any(r => r.UserId == x.Id));
    }

    public async Task<ApplicationUser?> FindByRefreshTokenAsync(string refreshToken)
    {
        return (await DbContext.Set<RefreshToken>()
            .Include(x => x.User)
                .ThenInclude(x => x.Tenants)
           .Include(x => x.User)
                .ThenInclude(x => x.Roles)
                .ThenInclude(x => x.Role)
           .Include(x => x.User)
                .ThenInclude(x => x.RefreshToken)
           .FirstOrDefaultAsync(x => x.Value == refreshToken))?.User;
    }

    public async Task<IList<string>> GetRolesAsync(string id, int tenantId)
    {
        return await DbContext.Database.SqlQueryRaw<string>(
                @"
                    SELECT Name From AspNetRoles
                    WHERE Id IN (SELECT RoleId FROM AspNetUserRoles WHERE TenantId = {0} AND UserId = {1})
                ", tenantId.ToString(), id)
            .ToListAsync();
    }

    public async Task<IList<TOut>> FindByTenantAsync<TOut>(int tenantId, IProjection<ApplicationUser, string, TOut> projection)
    {
        return await GetQueryable()
            .Where(x => x.Tenants.Any(t => t.TenantId == tenantId))
            .Select(projection.GetProject().Expand())
            .ToListAsync();
    }
    
    public async Task<string> GetRoleInTenant(string id, int tenantId)
    {
        return DbContext.Database.SqlQueryRaw<string>(
                @"
                    SELECT Name From AspNetRoles
                    WHERE Id IN (SELECT RoleId FROM AspNetUserRoles WHERE TenantId = {0} AND UserId = {1})
                ", tenantId.ToString(), id)
            .FirstOrDefault();
    }
}