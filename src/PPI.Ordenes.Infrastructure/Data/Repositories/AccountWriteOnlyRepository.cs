using System;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Infrastructure.Data.Context;
using PPI.Ordenes.Infrastructure.Data.Repositories.Common;

namespace PPI.Ordenes.Infrastructure.Data.Repositories;
internal class AccountWriteOnlyRepository (SqlDbContext context)
    : BaseWriteOnlyRepository<Account, Guid>(context), IAccountWriteOnlyRepository
{

}

