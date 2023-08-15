using TemplatePOC.Core.Damages;
using Wolverine.Http;

namespace TemplatePOC
{
    public class DamageEndpoints
    {
        private readonly ILogger<DamageEndpoints> _logger;

        public DamageEndpoints(ILogger<DamageEndpoints> logger)
        {
            _logger = logger;
        }

        [WolverinePost("/damages/create")]
        public CreationResponse Handle(ReportDamagedShipment command)
        {
            _logger.LogDebug("Creating Default Damage Report");
            //Store object
            return new CreationResponse("/damages/1");
        }

        [WolverineGet("/damages/{id}")]
        public async Task<DamageReport> Handle(Guid id)
        {
            _logger.LogDebug("Getting Default Damage Report");
            //Retrieve from storage
            return new DamageReport() {Id=1};
        }
    }
}
