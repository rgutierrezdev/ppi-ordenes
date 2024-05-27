using System;
using System.Threading.Tasks;
using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Domain.Entities.AccountAggregate;
public interface IAccountReadOnlyRepository : IReadOnlyRepository<Account, Guid>
{
    Task<Account> GetByAccountIdAsync(int accountId);
}
