using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Infrastructure.Data.Context;

namespace PPI.Ordenes.Infrastructure.Data.Repositories.Common;
internal abstract class BaseReadOnlyRepository<TEntity, Tkey>(SqlDbContext context) : IReadOnlyRepository<TEntity, Tkey>
    where TEntity : class, IEntity<Tkey>
    where Tkey : IEquatable<Tkey>
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly SqlDbContext Context = context;
    public async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await _dbSet.AsNoTrackingWithIdentityResolution().ToListAsync();
    public async Task<TEntity> GetByIdAsync(Tkey id) =>
        await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(entity => entity.Id.Equals(id));

    #region IDisposable

    // To detect redundant calls.
    private bool _disposed;

    ~BaseReadOnlyRepository() => Dispose(false);

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose managed state (managed objects).
        if (disposing)
            Context.Dispose();

        _disposed = true;
    }
    #endregion
}
