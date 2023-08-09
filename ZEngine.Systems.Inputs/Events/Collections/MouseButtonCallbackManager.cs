using Microsoft.Extensions.Logging;

namespace ZEngine.Systems.Inputs.Events.Collections;

public class MouseButtonCallbackManager : GenericCallbackManager<MouseButtonContext>
{
    public MouseButtonCallbackManager(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
}