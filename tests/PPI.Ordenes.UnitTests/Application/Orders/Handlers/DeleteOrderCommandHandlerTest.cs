using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Application.Order.Handlers;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Infrastructure.Data;
using PPI.Ordenes.UnitTests.Fixtures;
using Xunit;

namespace PPI.Ordenes.UnitTests.Application.Orders.Handlers;
public class DeleteOrderCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
{
    private readonly DeleteOrderCommandValidator _validator = new();

    [Fact]
    public async Task Delete_ValidOrderCommand_ShouldReturnsSuccessResult()
    {
        // Arrange
        var order = new Faker<Order>()
            .RuleFor(order => order.IDOrden, faker => faker.Random.Int(1))
            .Generate();

        var repository = Substitute.For<IOrderWriteOnlyRepository>();
        repository.GetByIdOrdenAsync(order.IDOrden).Returns(Task.FromResult(order)); 

        var unitOfWork = new UnitOfWork(fixture.Context,
                                        Substitute.For<IEventStoreRepository>(),
                                        Substitute.For<IMediator>(),
                                        Substitute.For<ILogger<UnitOfWork>>());

        var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
        unitOfWorkSubstitute.SaveChangesAsync().Returns(Task.CompletedTask); 

        var command = new Faker<DeleteOrderCommand>()
            .CustomInstantiator(faker => new DeleteOrderCommand(order.IDOrden))
            .Generate();

        var handler = new DeleteOrderCommandHandler(_validator, repository, unitOfWorkSubstitute);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        act.SuccessMessage.Should().Be("Orden eliminada correctamente.");
    }
}
