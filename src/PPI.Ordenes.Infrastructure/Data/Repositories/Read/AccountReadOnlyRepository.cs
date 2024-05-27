using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Infrastructure.Data.Context;
using PPI.Ordenes.Infrastructure.Data.Repositories.Common;

namespace PPI.Ordenes.Infrastructure.Data.Repositories.Read;
internal class AccountReadOnlyRepository(SqlDbContext context)
    : BaseReadOnlyRepository<Account, Guid>(context), IAccountReadOnlyRepository
{
    private readonly DbSet<Account> _dbSet = context.Set<Account>();
    public async Task<Account> GetByAccountIdAsync(int accountId) =>
        await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(entity => entity.IDCuenta.Equals(accountId));
}
