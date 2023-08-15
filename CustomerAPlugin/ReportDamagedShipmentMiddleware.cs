using Microsoft.Extensions.Logging;
using TemplatePOC.Core.Damages;
using Wolverine;

namespace CustomerAPlugin;

public class CustomerDamageReportingRequirements
{
    public double MinimumDamageReportingThreshold { get; set; }
    public string PrimaryContact { get; set; }

}

public class ReportDamagedShipmentMiddleware
{
    public (HandlerContinuation, CustomerDamageReportingRequirements?) Before(ReportDamagedShipment e, ILogger logger)
    {
        logger.LogDebug("Plugin is running before");

        //retrieve from DB but hardcoded for example here
        var reportingRequirements = new CustomerDamageReportingRequirements() { MinimumDamageReportingThreshold = 400, PrimaryContact = "stuffbroke@customer.com"};

        if (e.EstimatedDamageCost > e.ValueOfCargo)
            return (HandlerContinuation.Stop, null);

        if (e.EstimatedDamageCost > reportingRequirements.MinimumDamageReportingThreshold)
            return (HandlerContinuation.Continue, reportingRequirements);
        
        return (HandlerContinuation.Continue, null);
    }

    public void After(ReportDamagedShipment e, ILogger logger, CustomerDamageReportingRequirements? reporting)
    {
        if (reporting != null)
        {
            //send email to customer to notify of damages
        }

        logger.LogDebug("Plugin is running after");
    }
}