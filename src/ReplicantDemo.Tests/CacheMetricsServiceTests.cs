using Microsoft.Extensions.Logging;
using Moq;
using ReplicantDemo.Infrastructure.Services;
using Xunit;

namespace ReplicantDemo.Tests;

public class CacheMetricsServiceTests
{
    private readonly CacheMetricsService _service;
    private readonly Mock<ILogger<CacheMetricsService>> _loggerMock;

    public CacheMetricsServiceTests()
    {
        _loggerMock = new Mock<ILogger<CacheMetricsService>>();
        _service = new CacheMetricsService(_loggerMock.Object);
    }

    [Fact]
    public async Task GetMetrics_ShouldCalculateCorrectHitRate()
    {
        // Arrange
        await _service.RecordHitAsync();
        await _service.RecordHitAsync();
        await _service.RecordMissAsync();

        // Act
        var metrics = await _service.GetMetricsAsync();

        // Assert
        Assert.Equal(2, metrics.Hits);
        Assert.Equal(1, metrics.Misses);
        Assert.Equal(3, metrics.TotalRequests);
        Assert.Equal(2.0/3.0, metrics.HitRate);
    }

    [Fact]
    public async Task GetMetrics_WithNoRequests_ShouldReturnZeroHitRate()
    {
        // Act
        var metrics = await _service.GetMetricsAsync();

        // Assert
        Assert.Equal(0, metrics.Hits);
        Assert.Equal(0, metrics.Misses);
        Assert.Equal(0, metrics.TotalRequests);
        Assert.Equal(0, metrics.HitRate);
    }
}