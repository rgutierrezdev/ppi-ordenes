using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PPI.Ordenes.Core.SharedKernel;
using PPI.Ordenes.Domain.Entities.AccountAggregate;
using PPI.Ordenes.Domain.Entities.OrderAggregate;
using PPI.Ordenes.Infrastructure.Data;
using PPI.Ordenes.Infrastructure.Data.Context;
using PPI.Ordenes.Infrastructure.Data.Repositories;
using PPI.Ordenes.Infrastructure.Data.Repositories.Read;
using PPI.Ordenes.Infrastructure.Data.Services;

namespace PPI.Ordenes.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    /// <summary>
    /// Adds the memory cache service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddMemoryCacheService(this IServiceCollection services) =>
        services.AddScoped<ICacheService, MemoryCacheService>();

    /// <summary>
    /// Adds the distributed cache service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddDistributedCacheService(this IServiceCollection services) =>
        services.AddScoped<ICacheService, DistributedCacheService>();

    /// <summary>
    /// Adds the infrastructure services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddScoped<SqlDbContext>()
            .AddScoped<EventStoreDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

    /// <summary>
    /// Adds the write-only repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddWriteOnlyRepositories(this IServiceCollection services) =>
         services
            .AddScoped<IEventStoreRepository, EventStoreRepository>()            
            .AddScoped<IOrderWriteOnlyRepository, OrderWriteOnlyRepository>()
            .AddScoped<IAccountWriteOnlyRepository, AccountWriteOnlyRepository>()
            .AddScoped<IAccountReadOnlyRepository, AccountReadOnlyRepository>();
}