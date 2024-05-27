using System;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Domain.Factories;
using Xunit;
using Xunit.Categories;

namespace PPI.Ordenes.UnitTests.Domain.Entities.OrderAggregate;
[UnitTest]
public class OrderTests
{
    [Fact]
    public void Should_CreateOrder_WithCorrectProperties()
    {
        // Arrange
        var idCuenta = 1;
        var nombreActivo = "Test Asset";
        var cantidad = 10;
        var precio = 100m;
        var operacion = 'C';
        var estado = EOrderStatus.EnProceso;
        var montoTotal = 1000m;
        var fechaCreacion = DateTime.Now;

        // Act
        var result = OrderFactory.Create(idCuenta, nombreActivo, cantidad, precio, operacion, estado, montoTotal, fechaCreacion);

        // Assert
        var order = result.Value;
        Assert.Equal(idCuenta, order.IDCuenta);
        Assert.Equal(nombreActivo, order.NombreActivo);
        Assert.Equal(cantidad, order.Cantidad);
        Assert.Equal(precio, order.Precio);
        Assert.Equal(operacion, order.Operacion);
        Assert.Equal(estado, order.Estado);
        Assert.Equal(montoTotal, order.MontoTotal);
        Assert.Equal(fechaCreacion, order.FechaCreacion);
    }
}
