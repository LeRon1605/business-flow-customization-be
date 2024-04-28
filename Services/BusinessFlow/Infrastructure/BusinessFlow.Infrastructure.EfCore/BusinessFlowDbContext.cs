using BuildingBlocks.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessFlow.Infrastructure.EfCore;

public class BusinessFlowDbContext : AppDbContext
{
    public BusinessFlowDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BusinessFlowDbContext).Assembly);
    }
}