using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using PPI.Ordenes.Core.SharedKernel.Correlation;

namespace PPI.Ordenes.PublicApi.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderKey = "X-Correlation-Id";

    public async Task Invoke(HttpContext httpContext, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationId(httpContext, correlationIdGenerator);

        httpContext.Response.OnStarting(() =>
        {
            httpContext.Response.Headers.Append(CorrelationIdHeaderKey, new[] { correlationId.ToString() });
            return Task.CompletedTask;
        });

        await next(httpContext);
    }

    private static StringValues GetCorrelationId(HttpContext httpContext, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }

        return correlationIdGenerator.Get();
    }
}