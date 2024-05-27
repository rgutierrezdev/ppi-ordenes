using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using PPI.Ordenes.Application.Order.Handlers;
using PPI.Ordenes.Application.Order.Queries;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using Xunit;

namespace PPI.Ordenes.UnitTests.Application.Orders.Handlers;
public class GetAllOrderCommandHandlerTest
{
    [Fact]
    public async Task GetAll_ValidOrderCommand_ShouldReturnsSuccessResult()
    {
        // Arrange
        var repository = Substitute.For<IOrderWriteOnlyRepository>();
        repository.GetAllAsync().Returns(Task.FromResult<IEnumerable<Order>>([]));

        var handler = new GetAllOrderQueryHandler(repository);

        // Act
        var act = await handler.Handle(new GetAllOrderQuery(), CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        act.Value.Should().NotBeNull();
    }
}
