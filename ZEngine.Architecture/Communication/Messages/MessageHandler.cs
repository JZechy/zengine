using System.Reflection;

namespace ZEngine.Architecture.Communication.Messages;

/// <summary>
/// Class managing message receiving on behalf of object instance.
/// </summary>
public class MessageHandler
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
    private readonly object _instance;

    public MessageHandler(object instance)
    {
        Type type = instance.GetType();
        if (!type.IsAssignableTo(typeof(IMessageReceiver)))
        {
            throw new ArgumentException("Object instance does not implement IMessageReceiver interface.");
        }

        _instance = instance;
        _targets = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute<MessageTargetAttribute>() is not null || _systemTargets.Contains(x.Name))
            .Where(x => !x.GetParameters().Any()) // TODO: For now, only methods without parameters.
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

    /// <summary>
    /// Handles received message by invoking method with the same name as <paramref name="target"/>.
    /// </summary>
    /// <param name="target"></param>
    public void Handle(SystemMethod target)
    {
        Handle(target.ToString());
    }
}