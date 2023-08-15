using Microsoft.Extensions.Logging;
using TemplatePOC.Core.Damages;
using Wolverine.Http;

namespace CustomerAPlugin
{
    public class DamageReportEndpoint
    {
        private readonly ILogger<DamageReportEndpoint> _logger;

        public DamageReportEndpoint(ILogger<DamageReportEndpoint> logger)
        {
            _logger = logger;
        }

        [WolverineGet("reports/damage-report")]
        public List<DamageReport> Handle()
        {
            _logger.LogDebug("Getting Damage Report");
            //Retrieve from storage
            return new List<DamageReport>
            {
                new DamageReport() { Id=1},
                new DamageReport() { Id=2},
                new DamageReport() { Id=3}
            };
        }
    }
}
