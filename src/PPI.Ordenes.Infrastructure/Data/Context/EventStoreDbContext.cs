using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Infrastructure.Data.Mappings;

namespace PPI.Ordenes.Infrastructure.Data.Context;

public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> dbOptions)
    : BaseDbContext<EventStoreDbContext>(dbOptions)
{
    public DbSet<EventStore> EventStores => Set<EventStore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EventStoreConfiguration());
    }
}