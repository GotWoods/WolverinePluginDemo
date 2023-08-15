using JasperFx.Core;
using Microsoft.Extensions.Logging;
using TemplatePOC.Core.Damages;
using TemplatePOC.Core.Plugin;
using Wolverine;
using Wolverine.Http;

namespace CustomerAPlugin
{
    public class CustomerPlugin : IWolverineExtension
    {
        private readonly ILogger<CustomerPlugin> _logger;

        public CustomerPlugin(ILogger<CustomerPlugin> logger)
        {
            _logger = logger;
        }

        public void LoadEndpoints(WolverineHttpOptions opts)
        {
            // opts.AddPolicy<GetPolicies>();
        }

        public void Configure(WolverineOptions options)
        {
            _logger.LogDebug("Loading Default Middleware");
            options.Policies.ForMessagesOfType<ReportDamagedShipment>().AddMiddleware(typeof(ReportDamagedShipmentMiddleware));
        }
    }
}