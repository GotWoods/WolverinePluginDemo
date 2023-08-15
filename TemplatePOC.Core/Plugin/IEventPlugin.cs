using Wolverine;
using Wolverine.Http;

namespace TemplatePOC.Core.Plugin;

public interface IEventPlugin : IWolverineExtension
{
    void LoadEndpointPolicy(WolverineHttpOptions opts);
}