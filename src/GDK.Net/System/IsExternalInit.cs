// netstandard2.0 predates C# 9, so its reference assemblies do not declare the modreq type the
// compiler emits for `init` accessors and positional records. Declaring it here is the standard
// polyfill; on net5.0+ the framework's own definition is used instead.
#if !NET5_0_OR_GREATER

namespace System.Runtime.CompilerServices;

internal static class IsExternalInit
{
}

#endif
