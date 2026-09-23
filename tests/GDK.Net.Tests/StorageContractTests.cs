using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using GDK.Net;
using GDK.Net.Interop;
using GDK.Net.Storage;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Layout and argument-validation contracts for the persistent-local-storage family. These run
/// without a packaged title or a live Gaming Runtime.
/// </summary>
public class StorageContractTests
{
    [Fact]
    public void SpaceInfoMatchesTheHeaderLayout()
    {
        // XPersistentLocalStorageSpaceInfo is four uint64_t fields.
        Assert.Equal(32, Marshal.SizeOf<XPersistentLocalStorageSpaceInfo>());

        Assert.Equal(0, (int)Marshal.OffsetOf<XPersistentLocalStorageSpaceInfo>(
            nameof(XPersistentLocalStorageSpaceInfo.AvailableFreeBytes)));
        Assert.Equal(8, (int)Marshal.OffsetOf<XPersistentLocalStorageSpaceInfo>(
            nameof(XPersistentLocalStorageSpaceInfo.TotalFreeBytes)));
        Assert.Equal(16, (int)Marshal.OffsetOf<XPersistentLocalStorageSpaceInfo>(
            nameof(XPersistentLocalStorageSpaceInfo.UsedBytes)));
        Assert.Equal(24, (int)Marshal.OffsetOf<XPersistentLocalStorageSpaceInfo>(
            nameof(XPersistentLocalStorageSpaceInfo.TotalBytes)));
    }

    [Fact]
    public void PromptUserForSpaceNamesNoQueueOrRuntime()
    {
        // XPersistentLocalStoragePromptUserForSpaceAsync is a plain XAsyncBlock operation, so there
        // is nothing for a runtime or a queue to contribute: the block names no queue and the
        // Gaming Runtime resolves the process default.
        MethodInfo method = typeof(PersistentLocalStorage)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(m => m.Name == nameof(PersistentLocalStorage.PromptUserForSpaceAsync));

        Assert.Equal(
            new[] { typeof(ulong), typeof(CancellationToken) },
            method.GetParameters().Select(p => p.ParameterType));
    }
}
