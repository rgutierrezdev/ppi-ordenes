using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using PPI.Ordenes.Application.Order.Commands;
using PPI.Ordenes.Application.Order.Responses;
using PPI.Ordenes.Application.Security.Commands;
using PPI.Ordenes.Application.Security.Responses;
using PPI.Ordenes.Core.Extensions;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Infrastructure.Data.Context;
using PPI.Ordenes.PublicApi.Models;
using PPI.Ordenes.Query.Abstractions;
using PPI.Ordenes.Query.Data.Context;
using PPI.Ordenes.Query.Data.Repositories.Abstractions;
using PPI.Ordenes.Query.QueriesModel;
using Xunit;
using Xunit.Categories;

namespace PPI.Ordenes.IntegrationTests.Controllers.V1;

[IntegrationTest]
public class OrdersControllerTests : IAsyncLifetime
{
    private const string ConnectionString = "Data Source=:memory:";
    private const string Endpoint = "/api/orders";
    private const string TokenEndpoint = "/api/security";
    private readonly SqliteConnection _eventStoreDbContextSqlite = new(ConnectionString);
    private readonly SqliteConnection _writeDbContextSqlite = new(ConnectionString);

    [Fact]
    public async Task Should_ReturnsHttpStatus200Ok_When_Post_ValidRequest()
    {
        // Arrange
        await using var webApplicationFactory = InitializeWebAppFactory();
        using var httpClient = webApplicationFactory.CreateClient(CreateClientOptions());

        var command = new Faker<CreateOrderCommand>()
            .RuleFor(command => command.IdCuenta, 123)
            .RuleFor(command => command.NombreActivo, "TEST_ASSET")
            .RuleFor(command => command.Cantidad, faker => faker.Random.Int(1))
            .RuleFor(command => command.Operacion, faker => faker.PickRandom(new[] { 'C', 'V' }))
            .Generate();

        var tokenCommand = new Faker<CreateTokenCommand>()
            .RuleFor(command => command.UserName, command => command.Internet.UserName())
            .RuleFor(command => command.Password, command => command.Internet.Password())
            .Generate();

        var tokenAsJsonString = tokenCommand.ToJson();
        var commandAsJsonString = command.ToJson();

        var jsonContentToken = new StringContent(tokenAsJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        var responseToken = await httpClient.PostAsync(TokenEndpoint, jsonContentToken);

        var bearerToken = (await responseToken.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateTokenCommandResponse>>().Result.Token;
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        // Act
        using var jsonContent = new StringContent(commandAsJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        using var act = await httpClient.PostAsync(Endpoint, jsonContent);
        var responseCreateOrder = (await act.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateOrderResponse>>();

        // Assert (HTTP)
        act.Should().NotBeNull();
        act.IsSuccessStatusCode.Should().BeTrue();
        act.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert (HTTP Content Response)
        var response = (await act.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateOrderResponse>>();
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.StatusCode.Should().Be(StatusCodes.Status200OK);
        response.Errors.Should().BeEmpty();
        response.Result.Should().NotBeNull();        
    }

    private WebApplicationFactory<Program> InitializeWebAppFactory(
       Action<IServiceCollection> configureServices = null,
       Action<IServiceScope> configureServiceScope = null)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(hostBuilder =>
            {
                hostBuilder.UseSetting("ConnectionStrings:SqlConnection", "InMemory");
                hostBuilder.UseSetting("ConnectionStrings:NoSqlConnection", "InMemory");
                hostBuilder.UseSetting("ConnectionStrings:CacheConnection", "InMemory");

                hostBuilder.UseSetting("CacheOptions:AbsoluteExpirationInHours", "1");
                hostBuilder.UseSetting("CacheOptions:SlidingExpirationInSeconds", "30");

                hostBuilder.UseEnvironment(Environments.Production);                

                hostBuilder.ConfigureServices(services =>
                {
                    services.RemoveAll<SqlDbContext>();
                    services.RemoveAll<DbContextOptions<SqlDbContext>>();
                    services.RemoveAll<EventStoreDbContext>();
                    services.RemoveAll<DbContextOptions<EventStoreDbContext>>();
                    services.RemoveAll<NoSqlDbContext>();
                    services.RemoveAll<ISynchronizeDb>();

                    services.AddDbContext<SqlDbContext>(
                        options => options.UseSqlite(_writeDbContextSqlite));

                    services.AddDbContext<EventStoreDbContext>(
                        options => options.UseSqlite(_eventStoreDbContextSqlite));

                    services.AddSingleton(_ => Substitute.For<IReadDbContext>());
                    services.AddSingleton(_ => Substitute.For<ISynchronizeDb>());

                    var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                    unitOfWorkSubstitute.SaveChangesAsync().Returns(Task.CompletedTask);

                    var assetReadOnlyRepository = Substitute.For<IAssetReadOnlyRepository>();
                    var accountReadOnlyRepository = Substitute.For<IAccountReadOnlyRepository>();
                    assetReadOnlyRepository.GetByTickerAsync("TEST_ASSET").Returns(new AssetQueryModel(1, "TEST_ASSET", "Test Asset", 1, 100m));
                    accountReadOnlyRepository.GetByAccountIdAsync(123).Returns(new Account());

                    services.AddSingleton(_ => unitOfWorkSubstitute);
                    services.AddSingleton(_ => assetReadOnlyRepository);
                    services.AddSingleton(_ => accountReadOnlyRepository);


                    configureServices?.Invoke(services);

                    using var serviceProvider = services.BuildServiceProvider(true);
                    using var serviceScope = serviceProvider.CreateScope();

                    var writeDbContext = serviceScope.ServiceProvider.GetRequiredService<SqlDbContext>();
                    writeDbContext.Database.EnsureCreated();

                    var eventStoreDbContext = serviceScope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
                    eventStoreDbContext.Database.EnsureCreated();

                    configureServiceScope?.Invoke(serviceScope);

                    writeDbContext.Dispose();
                    eventStoreDbContext.Dispose();
                });
            });
    }

    private static WebApplicationFactoryClientOptions CreateClientOptions() => new() { AllowAutoRedirect = false };
    public async Task InitializeAsync()
    {
        await _writeDbContextSqlite.OpenAsync();
        await _eventStoreDbContextSqlite.OpenAsync();
    }

    public async Task DisposeAsync()
    {
        await _writeDbContextSqlite.DisposeAsync();
        await _eventStoreDbContextSqlite.DisposeAsync();
    }
}
