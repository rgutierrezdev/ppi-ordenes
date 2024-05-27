using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using PPI.Ordenes.Application.Order.Handlers;
using PPI.Ordenes.Application.Order.Queries;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Domain.Factories;
using Xunit;

namespace PPI.Ordenes.UnitTests.Application.Orders.Handlers;
public class GetOrderByIdCommandHandlerTest
{
    [Fact]
    public async Task GetOrderById_ValidOrderCommand_ShouldReturnsSuccessResult()
    {
        // Arrange
        var idOden = 0;        
        var idCuenta = 1;
        var nombreActivo = "Test Asset";
        var cantidad = 10;
        var precio = 100m;
        var operacion = 'C';
        var estado = EOrderStatus.EnProceso;
        var montoTotal = 1000m;
        var fechaCreacion = DateTime.Now;
                
        var order = OrderFactory.Create(idCuenta, nombreActivo, cantidad, precio, operacion, estado, montoTotal, fechaCreacion).Value;

        var repository = Substitute.For<IOrderWriteOnlyRepository>();
        repository.GetByIdOrdenAsync(idOden).Returns(Task.FromResult(order));

        var handler = new GetOrderByIdQueryHandler(repository);

        // Act
        var act = await handler.Handle(new GetOrderByIdQuery(idOden), CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();        
    }
}
