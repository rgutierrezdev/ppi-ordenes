using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Infrastructure.Data.Extensions;

namespace PPI.Ordenes.Infrastructure.Data.Mappings;
internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ConfigureBaseEntity();

        builder
            .Property(account => account.IDCuenta)
            .IsRequired(); // NOT NULL

        builder
            .Property(account => account.PrimerNombre)
            .IsRequired() // NOT NULL
            .HasMaxLength(30);

        builder
            .Property(account => account.SegundoNombre)
            .HasMaxLength(30);

        builder
            .Property(account => account.TercerNombre)
            .HasMaxLength(30);

        builder
            .Property(account => account.Apellido)
            .IsRequired() // NOT NULL
            .HasMaxLength(30);

        builder
            .Property(account => account.CuentaComitente)
            .IsRequired() // NOT NULL
            .HasColumnType("DECIMAL(18,0)");

        builder
            .Property(account => account.Estado)
            .IsRequired() // NOT NULL
            .HasConversion<int>();

        builder
            .Property(account => account.FechaCreacion)
            .IsRequired() // NOT NULL
            .HasColumnType("DATETIME");

        builder
            .Property(account => account.FechaActualizacion)
            .HasColumnType("DATETIME");
    }
}
