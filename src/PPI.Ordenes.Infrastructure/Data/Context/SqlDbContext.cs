using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Infrastructure.Data.Mappings;

namespace PPI.Ordenes.Infrastructure.Data.Context;

public class SqlDbContext(DbContextOptions<SqlDbContext> dbOptions)
    : BaseDbContext<SqlDbContext>(dbOptions)
{
    #region Encryption

    private static readonly byte[] EncryptionKey = [189, 3, 80, 118, 242, 164, 9, 197, 106, 166, 122, 118, 161, 212, 106, 26, 171, 18, 178, 98, 86, 102, 196, 6, 78, 249, 4, 164, 66, 154, 218, 126];
    private static readonly byte[] EncryptionVector = [163, 225, 4, 33, 227, 178, 113, 128, 174, 23, 9, 144, 213, 158, 134, 48];

    private readonly IEncryptionProvider _encryptionProvider = new AesProvider(EncryptionKey, EncryptionVector);

    #endregion
    
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.UseEncryption(_encryptionProvider);
    }
}