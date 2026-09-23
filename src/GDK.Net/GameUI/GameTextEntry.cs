using System;
using GDK.Net.Interop;

namespace GDK.Net.GameUI;

/// <summary>
/// An open non-modal text-entry session. Wraps <c>XGameUiTextEntryHandle</c>.
/// </summary>
/// <remarks>
/// Obtain an instance from <see cref="GameUiManager.OpenTextEntry"/>. Poll <see cref="GetState"/>
/// each frame to read user input; dispose the instance to close the IME panel
/// (<c>XGameUiTextEntryClose</c>).
/// </remarks>
public sealed unsafe class GameTextEntry : IDisposable
{
    private IntPtr _handle;
    private bool _disposed;

    // HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER)
    private const int HResultInsufficientBuffer = unchecked((int)0x8007007A);
    private const int MaxTextBufferBytes = 65536;

    internal GameTextEntry(IntPtr handle)
    {
        _handle = handle;
    }

    /// <summary>
    /// Reads the current text, cursor position and change flags
    /// (<c>XGameUiTextEntryGetState</c>).
    /// </summary>
    /// <remarks>
    /// Call once per frame. The text buffer doubles on <c>ERROR_INSUFFICIENT_BUFFER</c>, up to
    /// <c>64 KB</c>.
    /// </remarks>
    public TextEntryState GetState()
    {
        ThrowIfDisposed();
        IntPtr h = _handle;
        int capacity = 256;

        while (true)
        {
            byte[] buffer = new byte[capacity];
            XGameUiTextEntryChangeTypeFlags changeType = default;
            uint cursor = 0;
            uint imeStart = 0;
            uint imeEnd = 0;
            int hr;
            string text;

            fixed (byte* ptr = buffer)
            {
                hr = Native.XGameUiTextEntryGetState(
                    h,
                    &changeType,
                    &cursor,
                    &imeStart,
                    &imeEnd,
                    (uint)capacity,
                    ptr);

                if (hr == HResultInsufficientBuffer && capacity < MaxTextBufferBytes)
                {
                    capacity *= 2;
                    continue;
                }

                Hr.ThrowIfFailed(hr);
                text = Utf8.ToString(ptr) ?? string.Empty;
            }

            return new TextEntryState((TextEntryChangeTypeFlags)changeType, cursor, imeStart, imeEnd, text);
        }
    }

    /// <summary>
    /// Returns the current screen-space bounds of the IME panel
    /// (<c>XGameUiTextEntryGetExtents</c>).
    /// </summary>
    public TextEntryExtents GetExtents()
    {
        ThrowIfDisposed();
        XGameUiTextEntryExtents native = default;
        Hr.ThrowIfFailed(Native.XGameUiTextEntryGetExtents(_handle, &native));
        return new TextEntryExtents(native.left, native.top, native.right, native.bottom);
    }

    /// <summary>
    /// Moves the panel to the specified screen edge
    /// (<c>XGameUiTextEntryUpdatePositionHint</c>).
    /// </summary>
    /// <param name="positionHint">Target screen edge.</param>
    public void UpdatePositionHint(TextEntryPositionHint positionHint)
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(
            Native.XGameUiTextEntryUpdatePositionHint(_handle, (XGameUiTextEntryPositionHint)positionHint));
    }

    /// <summary>
    /// Updates candidate window visibility (<c>XGameUiTextEntryUpdateVisibility</c>).
    /// </summary>
    /// <param name="visibilityFlags">New visibility flags.</param>
    public void UpdateVisibility(TextEntryVisibilityFlags visibilityFlags)
    {
        ThrowIfDisposed();
        Hr.ThrowIfFailed(
            Native.XGameUiTextEntryUpdateVisibility(_handle, (XGameUiTextEntryVisibilityFlags)visibilityFlags));
    }

    /// <summary>Closes the IME panel (<c>XGameUiTextEntryClose</c>).</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (_handle != IntPtr.Zero)
        {
            Native.XGameUiTextEntryClose(_handle);
            _handle = IntPtr.Zero;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(GameTextEntry));
        }
    }
}
