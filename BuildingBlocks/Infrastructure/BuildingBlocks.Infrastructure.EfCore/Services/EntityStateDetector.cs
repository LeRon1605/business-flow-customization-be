using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlocks.Infrastructure.EfCore.Services;

public class EntityStateDetector : IEntityStateDetector
{
    private readonly ICurrentUser _currentUser;
    
    public EntityStateDetector(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public void ProcessEntityState(ChangeTracker changeTracker)
    {
        ProcessAuditEntityState(changeTracker);
        ProcessTenantEntityState(changeTracker);
        ProcessSoftDeleteEntityState(changeTracker);
    }
    
    private void ProcessTenantEntityState(ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IHasTenant>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.TenantId = _currentUser.TenantId;
                    break;
            }
        }
    }

    private void ProcessAuditEntityState(ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IHasAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Create(_currentUser.Id);
                    break;

                case EntityState.Modified:
                    entry.Entity.Update(_currentUser.Id);
                    break;
            }
        }
    }
    
    private void ProcessSoftDeleteEntityState(ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IHasSoftDelete>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.Delete(_currentUser.Id);
                    break;    
            }
        }
    }
}