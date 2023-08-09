using Microsoft.Extensions.Logging;

namespace ZEngine.Systems.Inputs.Events.Collections;

public class PointerPositionCallbackManager : GenericCallbackManager<PointerPositionContext>
{
    public PointerPositionCallbackManager(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
}