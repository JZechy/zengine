using ZEngine.Architecture.Components;
using ZEngine.Testing.System.Watchers;

namespace ZEngine.Testing.System;

/// <summary>
/// Exposed API to interact with the testing system for engine.
/// </summary>
public class TestManager
{
    /// <summary>
    /// Instance of the test manager.
    /// </summary>
    private static TestManager? _testManager;
    
    /// <summary>
    /// Access to the testing system.
    /// </summary>
    private readonly TestingSystem _testingSystem;

    private TestManager(TestingSystem testingSystem)
    {
        _testingSystem = testingSystem;
    }

    /// <summary>
    /// Singletion instance of the test manager.
    /// </summary>
    /// <exception cref="InvalidOperationException">Testing System was not initialized.</exception>
    public static TestManager Instance
    {
        get
        {
            if (_testManager is null)
            {
                throw new InvalidOperationException("Test manager is not initialized");
            }

            return _testManager;
        }
        private set => _testManager = value;
    }

    /// <summary>
    /// Creates aa new instance of the test manager.
    /// </summary>
    /// <param name="testingSystem"></param>
    internal static void CreateInstance(TestingSystem testingSystem)
    {
        Instance = new TestManager(testingSystem);
    }

    /// <summary>
    /// Creates an watcher that is awaiting the predicate to be met.
    /// </summary>
    /// <param name="component">Instance of component.</param>
    /// <param name="predicate">Predicate function to check the value.</param>
    /// <param name="timeoutMs">To prevent never-ending wait, timeout will cancel after certain time.</param>
    /// <typeparam name="TGameComponent"></typeparam>
    /// <returns></returns>
    public static IWatcher CreateComponentPredicateWatcher<TGameComponent>(TGameComponent component, Func<TGameComponent, bool> predicate, int timeoutMs = 1000)
        where TGameComponent : IGameComponent
    {
        ComponentPredicateWatcher<TGameComponent> watcher = new(component, predicate, timeoutMs);
        Instance._testingSystem.RegisterWatcher(watcher);

        return watcher;
    }
}