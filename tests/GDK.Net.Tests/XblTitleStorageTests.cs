using System;
using System.Reflection;
using System.Runtime.InteropServices;
using GDK.Net.Interop;
using GDK.Net.XboxLive;
using Microsoft.Win32.SafeHandles;
using Xunit;

namespace GDK.Net.Tests;

/// <summary>
/// Contract tests for the XSAPI title storage projection. These do not load
/// <c>Microsoft.Xbox.Services.C.Thunks.dll</c>; they pin enum values, native layouts and public
/// API shape against GDK edition 260404.
/// </summary>
public sealed unsafe class XblTitleStorageTests
{
    [Theory]
    [InlineData(TitleStorageType.TrustedPlatformStorage, 0u)]
    [InlineData(TitleStorageType.GlobalStorage, 1u)]
    [InlineData(TitleStorageType.Universal, 2u)]
    public void StorageTypeMatchesHeader(TitleStorageType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblTitleStorageType)value);
    }

    [Theory]
    [InlineData(TitleStorageBlobType.Unknown, 0u)]
    [InlineData(TitleStorageBlobType.Binary, 1u)]
    [InlineData(TitleStorageBlobType.Json, 2u)]
    [InlineData(TitleStorageBlobType.Config, 3u)]
    public void BlobTypeMatchesHeader(TitleStorageBlobType value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblTitleStorageBlobType)value);
    }

    [Theory]
    [InlineData(TitleStorageETagMatchCondition.NotUsed, 0u)]
    [InlineData(TitleStorageETagMatchCondition.IfMatch, 1u)]
    [InlineData(TitleStorageETagMatchCondition.IfNotMatch, 2u)]
    public void ETagMatchConditionMatchesHeader(TitleStorageETagMatchCondition value, uint expected)
    {
        Assert.Equal(expected, (uint)value);
        Assert.Equal(expected, (uint)(XblTitleStorageETagMatchCondition)value);
    }

    [Fact]
    public void BlobMetadataBufferSizesMatchHeaderConstants()
    {
        Assert.Equal(771, XblTitleStorageBlobMetadata.BlobPathMaxLength);
        Assert.Equal(387, XblTitleStorageBlobMetadata.BlobDisplayNameMaxLength);
        Assert.Equal(54, XblTitleStorageBlobMetadata.BlobETagMaxLength);
        Assert.Equal(40, XblTitleStorageBlobMetadata.ServiceConfigurationIdLength);
    }

    [Fact]
    public void BlobMetadataStructSizeMatchesHeader()
    {
        Assert.Equal(1288, sizeof(XblTitleStorageBlobMetadata));
    }

    [Fact]
    public void BlobMetadataFieldOffsetsMatchHeader()
    {
        var metadata = default(XblTitleStorageBlobMetadata);
        byte* origin = (byte*)&metadata;

        Assert.Equal(0, (int)(metadata.BlobPath - origin));
        Assert.Equal(772, (int)((byte*)&metadata.BlobType - origin));
        Assert.Equal(776, (int)((byte*)&metadata.StorageType - origin));
        Assert.Equal(780, (int)(metadata.DisplayName - origin));
        Assert.Equal(1167, (int)(metadata.ETag - origin));
        Assert.Equal(1224, (int)((byte*)&metadata.ClientTimestamp - origin));
        Assert.Equal(1232, (int)((byte*)&metadata.Length - origin));
        Assert.Equal(1240, (int)(metadata.ServiceConfigurationId - origin));
        Assert.Equal(1280, (int)((byte*)&metadata.XboxUserId - origin));
    }

    [Fact]
    public void TitleStorageEntryPointsAreDeclared()
    {
        Type native = typeof(GameRuntime).Assembly.GetType("GDK.Net.Interop.NativeXbl", throwOnError: true)!;
        string[] names =
        {
            "XblTitleStorageGetQuotaAsync",
            "XblTitleStorageGetQuotaResult",
            "XblTitleStorageGetBlobMetadataAsync",
            "XblTitleStorageGetBlobMetadataResult",
            "XblTitleStorageBlobMetadataResultGetItems",
            "XblTitleStorageBlobMetadataResultHasNext",
            "XblTitleStorageBlobMetadataResultGetNextAsync",
            "XblTitleStorageBlobMetadataResultGetNextResult",
            "XblTitleStorageBlobMetadataResultDuplicateHandle",
            "XblTitleStorageBlobMetadataResultCloseHandle",
            "XblTitleStorageDeleteBlobAsync",
            "XblTitleStorageDownloadBlobAsync",
            "XblTitleStorageDownloadBlobResult",
            "XblTitleStorageUploadBlobAsync",
            "XblTitleStorageUploadBlobResult",
        };

        foreach (string name in names)
        {
            Assert.NotNull(native.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static));
        }
    }

    [Fact]
    public void PublicTitleStorageTypesAreSealed()
    {
        Assert.True(typeof(TitleStorageBlobMetadata).IsSealed);
        Assert.True(typeof(TitleStorageBlobDownloadResult).IsSealed);
        Assert.True(typeof(TitleStorageBlobMetadataPage).IsSealed);
        Assert.True(typeof(TitleStorageService).IsSealed);
    }

    [Fact]
    public void TitleStorageHandleOwningTypesAreDisposable() =>
        Assert.True(typeof(IDisposable).IsAssignableFrom(typeof(TitleStorageBlobMetadataPage)));

    [Fact]
    public void TitleStorageServicesAreNotPubliclyConstructible()
    {
        Assert.Empty(typeof(TitleStorageService).GetConstructors());
        Assert.Empty(typeof(TitleStorageBlobMetadataPage).GetConstructors());
    }

    [Fact]
    public void TitleStorageServiceHasRequiredInternalConstructor()
    {
        ConstructorInfo? constructor = typeof(TitleStorageService).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            binder: null,
            new[] { typeof(XboxLiveContext) },
            modifiers: null);

        Assert.NotNull(constructor);
    }

    [Fact]
    public void PublicTitleStorageSurfaceExposesNoInteropTypes()
    {
        Type[] publicTypes =
        {
            typeof(TitleStorageQuota),
            typeof(TitleStorageBlobMetadata),
            typeof(TitleStorageBlobDownloadResult),
            typeof(TitleStorageBlobMetadataPage),
            typeof(TitleStorageService),
        };

        foreach (Type type in publicTypes)
        {
            foreach (MethodInfo method in type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly))
            {
                AssertNotInterop(type, method.Name, method.ReturnType);
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    AssertNotInterop(type, method.Name, parameter.ParameterType);
                }
            }
        }
    }

    [Fact]
    public void BlobMetadataConvertsToNativeFixedBuffers()
    {
        var metadata = new TitleStorageBlobMetadata(
            "folder\\blob.json",
            TitleStorageBlobType.Json,
            TitleStorageType.Universal,
            "00000000-0000-0000-0000-000000000000",
            "Save slot 1",
            "etag-1",
            DateTimeOffset.FromUnixTimeSeconds(123),
            42,
            2814670978532112);

        XblTitleStorageBlobMetadata native = metadata.ToNative();

        Assert.Equal("folder\\blob.json", Utf8.ToString(native.BlobPath, XblTitleStorageBlobMetadata.BlobPathMaxLength));
        Assert.Equal(XblTitleStorageBlobType.Json, native.BlobType);
        Assert.Equal(XblTitleStorageType.Universal, native.StorageType);
        Assert.Equal("Save slot 1", Utf8.ToString(native.DisplayName, XblTitleStorageBlobMetadata.BlobDisplayNameMaxLength));
        Assert.Equal("etag-1", Utf8.ToString(native.ETag, XblTitleStorageBlobMetadata.BlobETagMaxLength));
        Assert.Equal(123, native.ClientTimestamp);
        Assert.Equal((nuint)42, native.Length);
        Assert.Equal(
            "00000000-0000-0000-0000-000000000000",
            Utf8.ToString(native.ServiceConfigurationId, XblTitleStorageBlobMetadata.ServiceConfigurationIdLength));
        Assert.Equal(2814670978532112ul, native.XboxUserId);
    }

    [Fact]
    public void BlobMetadataRejectsValuesTooLargeForNativeBuffers()
    {
        string oversizedPath = new string('a', XblTitleStorageBlobMetadata.BlobPathMaxLength);
        var metadata = new TitleStorageBlobMetadata(
            oversizedPath,
            TitleStorageBlobType.Binary,
            TitleStorageType.Universal,
            "00000000-0000-0000-0000-000000000000");

        Assert.Throws<ArgumentException>(() => metadata.ToNative());
    }

    private static void AssertNotInterop(Type owner, string member, Type candidate)
    {
        Type target = candidate.IsByRef || candidate.IsPointer || candidate.IsArray
            ? candidate.GetElementType()!
            : candidate;

        Assert.False(
            target.Namespace is "GDK.Net.Interop",
            $"{owner.Name}.{member} exposes the interop type {target.Name}.");

        Assert.False(
            typeof(SafeHandle).IsAssignableFrom(target),
            $"{owner.Name}.{member} exposes the SafeHandle {target.Name}.");
    }
}
