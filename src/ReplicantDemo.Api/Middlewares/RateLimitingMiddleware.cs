using Microsoft.Extensions.Caching.Memory;

namespace ReplicantDemo.Api.Middlewares;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly ILogger<RateLimitingMiddleware> _logger;
    private readonly int _maxRequestsPerMinute;

    public RateLimitingMiddleware(
        RequestDelegate next,
        IMemoryCache cache,
        ILogger<RateLimitingMiddleware> logger,
        int maxRequestsPerMinute = 60)
    {
        _next = next;
        _cache = cache;
        _logger = logger;
        _maxRequestsPerMinute = maxRequestsPerMinute;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var cacheKey = $"rate_limit_{ipAddress}";

        if (!_cache.TryGetValue<Queue<DateTime>>(cacheKey, out var requestTimestamps))
        {
            requestTimestamps = new Queue<DateTime>();
        }

        // Clean up old timestamps
        while (requestTimestamps.Count > 0 && 
               DateTime.UtcNow - requestTimestamps.Peek() > TimeSpan.FromMinutes(1))
        {
            requestTimestamps.Dequeue();
        }

        if (requestTimestamps.Count >= _maxRequestsPerMinute)
        {
            _logger.LogWarning("Rate limit exceeded for IP: {IpAddress}", ipAddress);
            context.Response.StatusCode = 429; // Too Many Requests
            await context.Response.WriteAsJsonAsync(new
            {
                Error = "Too many requests. Please try again later."
            });
            return;
        }

        requestTimestamps.Enqueue(DateTime.UtcNow);
        _cache.Set(cacheKey, requestTimestamps, TimeSpan.FromMinutes(1));

        await _next(context);
    }
}