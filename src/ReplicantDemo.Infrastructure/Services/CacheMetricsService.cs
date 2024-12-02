using Microsoft.Extensions.Logging;

namespace ReplicantDemo.Infrastructure.Services;

public interface ICacheMetricsService
{
    Task<CacheMetrics> GetMetricsAsync();
    Task RecordHitAsync();
    Task RecordMissAsync();
}

public class CacheMetricsService : ICacheMetricsService
{
    private readonly ILogger<CacheMetricsService> _logger;
    private long _hits;
    private long _misses;

    public CacheMetricsService(ILogger<CacheMetricsService> logger)
    {
        _logger = logger;
    }

    public async Task<CacheMetrics> GetMetricsAsync()
    {
        var hits = Interlocked.Read(ref _hits);
        var misses = Interlocked.Read(ref _misses);
        var total = hits + misses;
        var hitRate = total > 0 ? (double)hits / total : 0;

        return new CacheMetrics
        {
            Hits = hits,
            Misses = misses,
            HitRate = hitRate,
            TotalRequests = total
        };
    }

    public Task RecordHitAsync()
    {
        Interlocked.Increment(ref _hits);
        return Task.CompletedTask;
    }

    public Task RecordMissAsync()
    {
        Interlocked.Increment(ref _misses);
        return Task.CompletedTask;
    }
}

public class CacheMetrics
{
    public long Hits { get; set; }
    public long Misses { get; set; }
    public double HitRate { get; set; }
    public long TotalRequests { get; set; }
}