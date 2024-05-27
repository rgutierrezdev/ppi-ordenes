using System;
using PPI.Ordenes.Core.SharedKernel;

namespace PPI.Ordenes.Domain.Entities.AccountAggregate;
public interface IAccountWriteOnlyRepository : IWriteOnlyRepository<Account, Guid>
{
}
