using Microsoft.AspNetCore.Mvc;
using ReplicantDemo.Infrastructure.Services;

namespace ReplicantDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MetricsController : ControllerBase
{
    private readonly ICacheMetricsService _metricsService;

    public MetricsController(ICacheMetricsService metricsService)
    {
        _metricsService = metricsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CacheMetrics), StatusCodes.Status200OK)]
    public async Task<ActionResult<CacheMetrics>> GetMetrics()
    {
        var metrics = await _metricsService.GetMetricsAsync();
        return Ok(metrics);
    }
}