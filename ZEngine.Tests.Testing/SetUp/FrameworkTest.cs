using Xunit.Abstractions;
using ZEngine.Testing;

namespace ZEngine.Tests.Testing.SetUp;

public class FrameworkTest : ZEngineFixture<FrameworkTestFactory>
{
    public FrameworkTest(FrameworkTestFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }
}