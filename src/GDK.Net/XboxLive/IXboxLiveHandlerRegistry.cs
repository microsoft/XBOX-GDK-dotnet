namespace GDK.Net.XboxLive;

/// <summary>
/// Implemented by the per-context registries that own native XSAPI notification-handler
/// registrations, so that <see cref="XboxLiveContext.Dispose"/> can tear every one of them down
/// before the underlying <c>XblContextHandle</c> is closed.
/// </summary>
/// <remarks>
/// XSAPI keys its notification handlers to the context handle and keeps calling them until they are
/// explicitly removed. A title that disposes a context while an event still has subscribers would
/// otherwise leave XSAPI holding callbacks against a closed handle, which is a use-after-free rather
/// than a leak. A registry therefore reports itself to the context the first time it registers
/// anything natively, and the context detaches it on the way out.
/// </remarks>
internal interface IXboxLiveHandlerRegistry
{
    /// <summary>
    /// Removes every native registration this registry currently owns and drops its managed
    /// subscribers. Called while the context handle is still valid, and must not throw: the context
    /// is already being torn down and a failed removal is not actionable.
    /// </summary>
    void DetachAll();
}
