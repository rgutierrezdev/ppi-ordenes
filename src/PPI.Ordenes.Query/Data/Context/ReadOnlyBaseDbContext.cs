using Microsoft.EntityFrameworkCore;

namespace PPI.Ordenes.Query.Data.Context;
public abstract class ReadOnlyBaseDbContext<TContext>(DbContextOptions<TContext> dbOptions) : DbContext(dbOptions) where TContext : DbContext
{
    private const string Collation = "Latin1_General_CI_AI";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation(Collation);

        base.OnModelCreating(modelBuilder);
    }
}
