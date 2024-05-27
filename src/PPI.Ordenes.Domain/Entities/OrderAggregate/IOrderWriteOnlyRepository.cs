using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Domain.Entities.OrderAggregate;
public interface IOrderWriteOnlyRepository : IWriteOnlyRepository<Order, Guid>
{
    Task<Order> GetByIdOrdenAsync(int idOrden);
    Task<IEnumerable<Order>> GetAllAsync();
}
