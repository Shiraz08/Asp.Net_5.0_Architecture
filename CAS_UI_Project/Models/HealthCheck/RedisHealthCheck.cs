using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CAS_UI_Project.Models.HealthCheck
{
    public class RedisHealthCheck:IHealthCheck
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public RedisHealthCheck(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = _connectionMultiplexer.GetDatabase();
                database.StringGet("health");
                return Task.FromResult(HealthCheckResult.Healthy());
            }
            catch(Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }
        }
    }
}
