using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Infrastructure.Data.Context;
using PPI.Ordenes.Infrastructure.Data.Repositories.Common;

namespace PPI.Ordenes.Infrastructure.Data.Repositories;
internal class OrderWriteOnlyRepository(SqlDbContext context)
    : BaseWriteOnlyRepository<Order, Guid>(context), IOrderWriteOnlyRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await Context.Orders.ToListAsync();

    public async Task<Order> GetByIdOrdenAsync(int idOrden) =>
        await Context.Orders            
            .FirstOrDefaultAsync(order => order.IDOrden == idOrden);
}
