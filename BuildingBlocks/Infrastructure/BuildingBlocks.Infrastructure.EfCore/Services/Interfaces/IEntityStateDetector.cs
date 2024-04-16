using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;

public interface IEntityStateDetector
{
    void ProcessEntityState(ChangeTracker changeTracker);
}