using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using GDK.Net.Interop;

namespace GDK.Net.XboxLive;

/// <summary>Xbox Live user-generated text verification. Mirrors <c>string_verify_c.h</c>.</summary>
/// <remarks>
/// <para>
/// Titles must verify user-generated text before display, including chat gamertags, custom names
/// and similar content. This projection makes that obligation fail-closed: if Xbox Live cannot
/// complete a check for any reason, including cancellation, missing native exports or result-buffer
/// failure, the returned <see cref="StringVerificationResult"/> objects have
/// <see cref="StringVerificationResult.IsAcceptable"/> <see langword="false"/>.
/// </para>
/// <para>
/// The plural API preserves Xbox Live's result ordering exactly: result index <c>i</c> corresponds
/// to input string index <c>i</c>, and each result also carries its
/// <see cref="StringVerificationResult.VerifiedString"/>.
/// </para>
/// </remarks>
public sealed unsafe class StringVerificationService
{
    private readonly XboxLiveContext _context;

    internal StringVerificationService(XboxLiveContext context) => _context = context;

    /// <summary>
    /// Verifies whether one user-generated string is acceptable for display
    /// (<c>XblStringVerifyStringAsync</c>).
    /// </summary>
    /// <param name="text">The string to verify. Empty strings are allowed.</param>
    /// <param name="cancellationToken">Cancels the native call; the returned result is unacceptable.</param>
    /// <returns>
    /// A fail-closed verification result. <see cref="StringVerificationResult.IsAcceptable"/> is
    /// <see langword="true"/> only when Xbox Live completed the check and explicitly returned
    /// <see cref="VerifyStringResultCode.Success"/>.
    /// </returns>
    public Task<StringVerificationResult> VerifyStringAsync(
        string text,
        CancellationToken cancellationToken = default)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        StringVerificationResult failClosed = StringVerificationResult.FailClosed(text);
        IntPtr textBuffer = IntPtr.Zero;

        try
        {
            textBuffer = Utf8.Allocate(text);
            IntPtr context = _context.Handle;

            return AsyncOperation<StringVerificationResult>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblStringVerifyStringAsync(
                    context,
                    (byte*)textBuffer,
                    (XAsyncBlock*)block),
                (IntPtr block, out StringVerificationResult value) =>
                {
                    Utf8.Free(textBuffer);
                    return ReadStringResult(block, text, failClosed, out value);
                },
                cancellationToken);
        }
        catch (Exception ex) when (IsVerificationCompletionFailure(ex))
        {
            Utf8.Free(textBuffer);
            return Task.FromResult(failClosed);
        }
        catch
        {
            Utf8.Free(textBuffer);
            throw;
        }
    }

    /// <summary>
    /// Verifies whether several user-generated strings are acceptable for display
    /// (<c>XblStringVerifyStringsAsync</c>).
    /// </summary>
    /// <param name="texts">The strings to verify. Empty strings are allowed.</param>
    /// <param name="cancellationToken">Cancels the native call; all returned results are unacceptable.</param>
    /// <returns>
    /// A fail-closed result for each input string, in the same order as <paramref name="texts"/>.
    /// <see cref="StringVerificationResult.IsAcceptable"/> is <see langword="true"/> only for
    /// entries Xbox Live explicitly verified as
    /// <see cref="VerifyStringResultCode.Success"/>.
    /// </returns>
    public Task<IReadOnlyList<StringVerificationResult>> VerifyStringsAsync(
        IEnumerable<string> texts,
        CancellationToken cancellationToken = default)
    {
        if (texts is null)
        {
            throw new ArgumentNullException(nameof(texts));
        }

        string[] textArray = ToStringArray(texts);
        IReadOnlyList<StringVerificationResult> failClosed = CreateFailClosedResults(textArray);

        if (textArray.Length == 0)
        {
            return Task.FromResult<IReadOnlyList<StringVerificationResult>>(
                Array.Empty<StringVerificationResult>());
        }

        NativeStringArray? nativeStrings = null;

        try
        {
            nativeStrings = new NativeStringArray(textArray);
            IntPtr context = _context.Handle;

            return AsyncOperation<IReadOnlyList<StringVerificationResult>>.RunAsync(
                _context.Queue.RawHandle(),
                block => NativeXbl.XblStringVerifyStringsAsync(
                    context,
                    nativeStrings.Pointer,
                    checked((ulong)textArray.Length),
                    (XAsyncBlock*)block),
                (IntPtr block, out IReadOnlyList<StringVerificationResult> value) =>
                {
                    nativeStrings.Dispose();
                    return ReadStringsResult(block, textArray, failClosed, out value);
                },
                cancellationToken);
        }
        catch (Exception ex) when (IsVerificationCompletionFailure(ex))
        {
            nativeStrings?.Dispose();
            return Task.FromResult(failClosed);
        }
        catch
        {
            nativeStrings?.Dispose();
            throw;
        }
    }

    private static int ReadStringResult(
        IntPtr block,
        string text,
        StringVerificationResult failClosed,
        out StringVerificationResult value)
    {
        value = failClosed;
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = NativeXbl.XblStringVerifyStringResultSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr) || size == 0)
            {
                return HResult.SOk;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblVerifyStringResult* result;
            nuint used;
            hr = NativeXbl.XblStringVerifyStringResult(
                (XAsyncBlock*)block,
                size,
                (void*)buffer,
                &result,
                &used);
            if (HResult.Failed(hr))
            {
                return HResult.SOk;
            }

            value = StringVerificationResult.FromNative(text, result);
            return HResult.SOk;
        }
        catch (Exception)
        {
            value = failClosed;
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static int ReadStringsResult(
        IntPtr block,
        string[] texts,
        IReadOnlyList<StringVerificationResult> failClosed,
        out IReadOnlyList<StringVerificationResult> value)
    {
        value = failClosed;
        IntPtr buffer = IntPtr.Zero;

        try
        {
            nuint size;
            int hr = NativeXbl.XblStringVerifyStringsResultSize((XAsyncBlock*)block, &size);
            if (HResult.Failed(hr) || size == 0)
            {
                return HResult.SOk;
            }

            buffer = Marshal.AllocHGlobal(new IntPtr(checked((long)size)));
            XblVerifyStringResult* results;
            nuint count;
            nuint used;
            hr = NativeXbl.XblStringVerifyStringsResult(
                (XAsyncBlock*)block,
                size,
                (void*)buffer,
                &results,
                &count,
                &used);
            if (HResult.Failed(hr) || count != (nuint)texts.Length)
            {
                return HResult.SOk;
            }

            var managed = new StringVerificationResult[(int)count];
            for (int i = 0; i < managed.Length; i++)
            {
                managed[i] = StringVerificationResult.FromNative(texts[i], results + i);
            }

            value = new ReadOnlyCollection<StringVerificationResult>(managed);
            return HResult.SOk;
        }
        catch (Exception)
        {
            value = failClosed;
            return HResult.SOk;
        }
        finally
        {
            Free(buffer);
        }
    }

    private static IReadOnlyList<StringVerificationResult> CreateFailClosedResults(string[] texts)
    {
        var results = new StringVerificationResult[texts.Length];
        for (int i = 0; i < texts.Length; i++)
        {
            results[i] = StringVerificationResult.FailClosed(texts[i]);
        }

        return new ReadOnlyCollection<StringVerificationResult>(results);
    }

    private static string[] ToStringArray(IEnumerable<string> texts)
    {
        string[] values;
        if (texts is string[] array)
        {
            values = array;
        }
        else if (texts is ICollection<string> collection)
        {
            values = new string[collection.Count];
            collection.CopyTo(values, 0);
        }
        else
        {
            values = new List<string>(texts).ToArray();
        }

        for (int i = 0; i < values.Length; i++)
        {
            if ((object?)values[i] is null)
            {
                throw new ArgumentException("Strings to verify cannot contain null.", nameof(texts));
            }
        }

        return values;
    }

    private static bool IsVerificationCompletionFailure(Exception exception) =>
        exception is GameRuntimeException or OperationCanceledException or DllNotFoundException
            or EntryPointNotFoundException or BadImageFormatException;

    private static void Free(IntPtr buffer)
    {
        if (buffer != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private sealed class NativeStringArray : IDisposable
    {
        private readonly IntPtr[] _buffers;
        private IntPtr _pointerArray;

        internal NativeStringArray(string[] strings)
        {
            _buffers = new IntPtr[strings.Length];
            _pointerArray = Marshal.AllocHGlobal(checked(strings.Length * IntPtr.Size));

            try
            {
                byte** pointers = (byte**)_pointerArray;
                for (int i = 0; i < strings.Length; i++)
                {
                    _buffers[i] = Utf8.Allocate(strings[i]);
                    pointers[i] = (byte*)_buffers[i];
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        internal byte** Pointer => (byte**)_pointerArray;

        public void Dispose()
        {
            for (int i = 0; i < _buffers.Length; i++)
            {
                Utf8.Free(_buffers[i]);
                _buffers[i] = IntPtr.Zero;
            }

            Free(_pointerArray);
            _pointerArray = IntPtr.Zero;
        }
    }
}
