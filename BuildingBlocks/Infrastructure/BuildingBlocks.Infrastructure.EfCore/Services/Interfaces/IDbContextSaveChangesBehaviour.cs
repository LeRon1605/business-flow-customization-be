using BuildingBlocks.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Services.Interfaces;

public interface IDbContextSaveChangesBehaviour
{
    Task<IList<IDomainEvent>> SaveChangesAsync(DbContext dbContext);
}