using Microsoft.Extensions.Diagnostics.HealthChecks;
using Replicant;

namespace ReplicantDemo.Api.Health;

public class ReplicantHealthCheck : IHealthCheck
{
    private readonly HttpCache _httpCache;

    public ReplicantHealthCheck(HttpCache httpCache)
    {
        _httpCache = httpCache;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Try to access the cache directory
            var cacheDirectory = _httpCache.GetType()
                .GetField("_directory", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_httpCache) as string;

            if (string.IsNullOrEmpty(cacheDirectory) || !Directory.Exists(cacheDirectory))
            {
                return HealthCheckResult.Unhealthy("Cache directory is not accessible");
            }

            return HealthCheckResult.Healthy("Cache system is operational");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Cache system check failed", ex);
        }
    }
}