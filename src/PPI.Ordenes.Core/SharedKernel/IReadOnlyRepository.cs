using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PPI.Ordenes.Core.SharedKernel;
public interface IReadOnlyRepository<TEntity, in TKey> : IDisposable
where TEntity : IEntity<TKey>
where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Retrieves all entities from the repository asynchronously.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The retrieved entity.</returns>
    Task<TEntity> GetByIdAsync(TKey id);
}
