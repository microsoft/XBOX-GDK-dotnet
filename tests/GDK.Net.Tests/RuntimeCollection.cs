using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Groups the tests that touch process-global runtime state so xUnit runs them one at a time.
/// </summary>
/// <remarks>
/// <see cref="GameRuntime.Dispose"/> drains the process-wide subsystem registry, so a test that
/// initializes a runtime on one thread would otherwise tear down the subsystems another test had
/// just registered. These tests are cheap; serializing them costs nothing measurable.
/// </remarks>
[CollectionDefinition(Name)]
public sealed class RuntimeCollection
{
    public const string Name = "process-global runtime state";
}
