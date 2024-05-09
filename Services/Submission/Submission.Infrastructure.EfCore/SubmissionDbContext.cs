using BuildingBlocks.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Submission.Infrastructure.EfCore;

public class SubmissionDbContext : AppDbContext
{
    public SubmissionDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubmissionDbContext).Assembly);
    }
}