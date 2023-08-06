using System.Reflection;

namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Class managing message receiving on behalf of <typeparamref name="TReceiver"/>.
/// </summary>
/// <typeparam name="TReceiver"></typeparam>
public class MessageHandler<TReceiver> where TReceiver : IMessageReceiver
{
    /// <summary>
    /// List of all system methods that can receive messages.
    /// </summary>
    private readonly HashSet<string> _systemTargets = Enum.GetNames<SystemMethod>().ToHashSet();

    /// <summary>
    /// List of all available methods that can receive messages.
    /// </summary>
    private readonly Dictionary<string, MethodInfo> _targets;

    /// <summary>
    /// Instance of the object, for which we are managing message receiving.
    /// </summary>
    private readonly TReceiver _instance;

    public MessageHandler(TReceiver instance)
    {
        _instance = instance;
        _targets = instance.GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<MessageTargetAttribute>() is not null || _systemTargets.Contains(x.Name))
            .Where(x => !x.GetParameters().Any()) // For now, only methods without parameters.
            .ToDictionary(x => x.Name, x => x);
    }

    /// <summary>
    /// Handles received message by invoking method with the same name as <paramref name="target"/>.
    /// </summary>
    /// <param name="target"></param>
    public void Handle(string target)
    {
        if (!_targets.TryGetValue(target, out MethodInfo? method))
        {
            return;
        }

        try
        {
            method.Invoke(_instance, null);
        }
        catch (Exception e)
        {
            throw new MessageHandlerException(e);
        }
    }
    
    public void Handle(SystemMethod target)
    {
        Handle(target.ToString());
    }
}