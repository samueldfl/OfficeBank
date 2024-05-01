using System.Threading.RateLimiting;

namespace Api.Settings;

internal static class RequestRateLimit
{
    public static IServiceCollection AddRequestRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddPolicy(
                "fixed",
                context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromSeconds(45)
                        }
                    )
            );

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode =
                    StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync(
                    "Too many requests. Please try later again...",
                    cancellationToken: token
                );
            };
        });

        return services;
    }
}
