using Bogus;
using NSubstitute;
using System.Threading.Tasks;
using System.Threading;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Application.Order.Handlers;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.UnitTests.Fixtures;
using Xunit;
using FluentAssertions;
using PPI.Ordenes.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PPI.Ordenes.UnitTests.Application.Orders.Handlers;
public class UpdateOrderCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
{
    private readonly UpdateOrderCommandValidator _validator = new();

    [Fact]
    public async Task Update_ValidOrderCommand_ShouldReturnsSuccessResult()
    {
        // Arrange
        var order = new Faker<Order>()
            .RuleFor(order => order.IDOrden, faker => faker.Random.Int(1))
            .Generate();

        var repository = Substitute.For<IOrderWriteOnlyRepository>();
        repository.GetByIdOrdenAsync(order.IDOrden).Returns(Task.FromResult(order)); // Simula que la orden existe

        var unitOfWork = new UnitOfWork(
           fixture.Context,
           Substitute.For<IEventStoreRepository>(),
           Substitute.For<IMediator>(),
           Substitute.For<ILogger<UnitOfWork>>());

        var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
        unitOfWorkSubstitute.SaveChangesAsync().Returns(Task.CompletedTask); // Simula que SaveChangesAsync se ejecuta correctamente

        var command = new Faker<UpdateOrderCommand>()
            .RuleFor(command => command.IdOrden, order.IDOrden)
            .RuleFor(command => command.Estado, faker => EOrderStatus.Ejecutada)
            .Generate();

        var handler = new UpdateOrderCommandHandler(_validator, repository, unitOfWorkSubstitute);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        act.SuccessMessage.Should().Be("Orden actualizada correctamente.");
    }
}
