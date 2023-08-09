using Microsoft.Extensions.Logging;

namespace ZEngine.Systems.Inputs.Events.Collections;

public class KeyboardInputCallbackManager : GenericCallbackManager<KeyboardContext>
{
    public KeyboardInputCallbackManager(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
}