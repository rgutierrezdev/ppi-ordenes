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
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Infrastructure.Data;
using PPI.Ordenes.Infrastructure.Data.Repositories;
using PPI.Ordenes.Query.Data.Repositories.Abstractions;
using PPI.Ordenes.Query.QueriesModel;
using PPI.Ordenes.UnitTests.Fixtures;
using Xunit;

namespace PPI.Ordenes.UnitTests.Application.Orders.Handlers;
public class CreateOrderCommandHandlerTests(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
{
    private readonly CreateOrderCommandValidator _validator = new();

    [Fact]
    public async Task Add_ValidCommand_ShouldReturnsSuccessResult()
    {
        // Arrange
        var command = new Faker<CreateOrderCommand>()
            .RuleFor(command => command.IdCuenta, faker => faker.Random.Int())
            .RuleFor(command => command.NombreActivo, faker => faker.Random.String(32))
            .RuleFor(command => command.Cantidad, faker => faker.Random.Int())
            .RuleFor(command => command.Operacion, faker => faker.PickRandom(new[] { 'C', 'V'}))
            .Generate();

        var unitOfWork = new UnitOfWork(
            fixture.Context,
            Substitute.For<IEventStoreRepository>(),
            Substitute.For<IMediator>(),
            Substitute.For<ILogger<UnitOfWork>>());

        var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
        unitOfWorkSubstitute.SaveChangesAsync().Returns(Task.CompletedTask); 

        var assetReadOnlyRepository = Substitute.For<IAssetReadOnlyRepository>();
        var accountReadOnlyRepository = Substitute.For<IAccountReadOnlyRepository>();        

        assetReadOnlyRepository.GetByTickerAsync(command.NombreActivo).Returns(new AssetQueryModel(1, command.NombreActivo, "Test Asset", 1, 100m));
        accountReadOnlyRepository.GetByAccountIdAsync(command.IdCuenta).Returns(new Account());
        
        var handler = new CreateOrderCommandHandler(
            _validator,
            new OrderWriteOnlyRepository(fixture.Context),
            assetReadOnlyRepository,
            accountReadOnlyRepository,
            unitOfWorkSubstitute);

        // Act
        var act = await handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().NotBeNull();
        act.IsSuccess.Should().BeTrue();
        act.SuccessMessage.Should().Be("Orden creada exitosamente!");
        act.Value.Should().NotBeNull();        
    }


}
