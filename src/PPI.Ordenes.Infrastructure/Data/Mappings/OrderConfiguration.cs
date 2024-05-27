using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Infrastructure.Data.Extensions;

namespace PPI.Ordenes.Infrastructure.Data.Mappings;
internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .ConfigureBaseEntity();

        builder
            .Property(order => order.IDOrden)
            .IsRequired() // NOT NULL
            .ValueGeneratedOnAdd();
        builder
            .Property(order => order.IDCuenta)
            .IsRequired(); // NOT NULL

        builder
            .Property(order => order.NombreActivo)
            .IsRequired() // NOT NULL
            .HasMaxLength(32);

        builder
            .Property(order => order.Cantidad)
            .IsRequired(); // NOT NULL

        builder
            .Property(order => order.Precio)
            .IsRequired() // NOT NULL
            .HasColumnType("DECIMAL(18,2)");

        builder
            .Property(order => order.Operacion)
            .IsRequired() // NOT NULL
            .HasMaxLength(1)
            .HasConversion<string>();

        builder
            .Property(order => order.Estado)
            .IsRequired() // NOT NULL
            .HasConversion<int>();

        builder
            .Property(order => order.MontoTotal)
            .IsRequired() // NOT NULL
            .HasColumnType("DECIMAL(18,2)");

        builder
            .Property(order => order.FechaCreacion)
            .IsRequired() // NOT NULL
            .HasColumnType("DATETIME");

        builder
            .Property(order => order.FechaActualizacion)
            .HasColumnType("DATETIME");

    }
}
