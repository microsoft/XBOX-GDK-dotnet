using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDK.Net.GameSave;

namespace GDK.Net.LiveHarness;

/// <summary>
/// Connected storage: provider initialisation, containers, and a full blob write/read/delete cycle.
/// </summary>
/// <remarks>
/// <para>
/// This family writes for real, and it has to. A read-only game-save check proves almost nothing:
/// the interesting marshalling is all on the write path — the blob array the update builds, the
/// pointers it hands to <c>XGameSaveSubmitUpdate</c>, and whether what comes back out of
/// <c>XGameSaveReadBlobData</c> is byte-for-byte what went in.
/// </para>
/// <para>
/// The container is named for the harness and deleted in teardown, so a run leaves the account's
/// save data as it found it even though it wrote to it along the way.
/// </para>
/// </remarks>
internal static class GameSaveChecks
{
    private const string ContainerName = "GdkNetLiveHarness";
    private const string BlobName = "roundtrip";
    private const string ProviderKey = "gamesave.provider";
    private const string ContainerKey = "gamesave.container";

    private static readonly byte[] Payload =
        Encoding.UTF8.GetBytes("GDK.Net live harness round-trip payload \u2014 non-ASCII included on purpose.");

    public static IEnumerable<LiveCheck> All()
    {
        yield return LiveCheck.Async("gamesave.provider", async ctx =>
        {
            GameSaveProvider provider = await GameSaveProvider
                .InitializeAsync(ctx.RequireUser, ctx.RequireScid)
                .ConfigureAwait(false);

            ctx.State[ProviderKey] = provider;
            return "XGameSaveInitializeProviderAsync succeeded for the signed-in user";
        }, "users.add", "runtime.title-id");

        yield return LiveCheck.Async("gamesave.quota", async ctx =>
        {
            long sync = Provider(ctx).GetRemainingQuota();
            long async = await Provider(ctx).GetRemainingQuotaAsync().ConfigureAwait(false);
            return $"XGameSaveGetRemainingQuota = {sync} bytes; the async form reported {async}";
        }, "gamesave.provider");

        yield return LiveCheck.Sync("gamesave.create-container", ctx =>
        {
            GameSaveContainer container = Provider(ctx).CreateContainer(ContainerName);
            ctx.State[ContainerKey] = container;
            return $"XGameSaveCreateContainer '{ContainerName}'";
        }, "gamesave.provider");

        yield return LiveCheck.Async("gamesave.write-blob", async ctx =>
        {
            using GameSaveUpdate update = Container(ctx).CreateUpdate($"{ContainerName} round-trip");
            update.Write(BlobName, Payload);
            await update.SubmitAsync().ConfigureAwait(false);
            return $"XGameSaveSubmitUpdateAsync wrote {Payload.Length} bytes to blob '{BlobName}'";
        }, "gamesave.create-container");

        yield return LiveCheck.Sync("gamesave.enumerate-blobs", ctx =>
        {
            IReadOnlyList<GameSaveBlobInfo> blobs = Container(ctx).EnumerateBlobs();
            GameSaveBlobInfo? written = blobs.FirstOrDefault(b => b.Name == BlobName);

            return written is null
                ? throw new InvalidOperationException(
                    $"XGameSaveEnumerateBlobInfo did not list '{BlobName}' after a successful submit. " +
                    $"It listed: {string.Join(", ", blobs.Select(b => b.Name))}")
                : $"XGameSaveEnumerateBlobInfo lists '{written.Name}' at {written.Size} bytes " +
                  $"({blobs.Count} blob(s) in the container)";
        }, "gamesave.write-blob");

        yield return LiveCheck.Async("gamesave.read-blob", async ctx =>
        {
            IReadOnlyList<GameSaveBlob> blobs = await Container(ctx)
                .ReadBlobsAsync([BlobName]).ConfigureAwait(false);

            if (blobs.Count != 1)
            {
                throw new InvalidOperationException(
                    $"XGameSaveReadBlobDataAsync asked for one blob and returned {blobs.Count}.");
            }

            return blobs[0].Data.AsSpan().SequenceEqual(Payload)
                ? $"XGameSaveReadBlobDataAsync returned all {Payload.Length} bytes byte-for-byte"
                : throw new InvalidOperationException(
                    $"Blob round-trip corrupted the payload: wrote {Payload.Length} bytes, " +
                    $"read back {blobs[0].Data.Length}.");
        }, "gamesave.write-blob");

        yield return LiveCheck.Sync("gamesave.container-info", ctx =>
        {
            GameSaveContainerInfo info = Provider(ctx).GetContainerInfo(ContainerName);
            IReadOnlyList<GameSaveContainerInfo> byPrefix =
                Provider(ctx).EnumerateContainersByPrefix(ContainerName);

            return byPrefix.Any(c => c.Name == info.Name)
                ? $"XGameSaveGetContainerInfo and XGameSaveEnumerateContainerInfoByName agree on " +
                  $"'{info.Name}' (display '{info.DisplayName}', {info.TotalSize} bytes, " +
                  $"changed {info.LastModified:u}, needsSync={info.NeedsSync})"
                : throw new InvalidOperationException(
                    $"XGameSaveEnumerateContainerInfoByName did not return '{ContainerName}' by its own prefix.");
        }, "gamesave.write-blob");

        yield return LiveCheck.Async("gamesave.delete-blob", async ctx =>
        {
            using GameSaveUpdate update = Container(ctx).CreateUpdate($"{ContainerName} cleanup");
            update.Delete(BlobName);
            await update.SubmitAsync().ConfigureAwait(false);

            IReadOnlyList<GameSaveBlobInfo> blobs = Container(ctx).EnumerateBlobs();
            return blobs.Any(b => b.Name == BlobName)
                ? throw new InvalidOperationException(
                    $"XGameSaveSubmitUpdateAsync reported success but '{BlobName}' is still enumerated.")
                : $"XGameSaveSubmitUpdate deleted '{BlobName}'; {blobs.Count} blob(s) remain";
        }, "gamesave.read-blob");
    }

    /// <summary>
    /// Closes the handles and removes the container the run created, so the account's save data is
    /// left as it was found.
    /// </summary>
    public static IEnumerable<LiveCheck> Teardown()
    {
        yield return LiveCheck.Sync("gamesave.container.dispose", ctx =>
        {
            Container(ctx).Dispose();
            ctx.State.Remove(ContainerKey);
            return "XGameSaveCloseContainer";
        }, "gamesave.create-container");

        yield return LiveCheck.Async("gamesave.delete-container", async ctx =>
        {
            await Provider(ctx).DeleteContainerAsync(ContainerName).ConfigureAwait(false);
            return $"XGameSaveDeleteContainerAsync removed '{ContainerName}'";
        }, "gamesave.create-container");

        yield return LiveCheck.Sync("gamesave.provider.dispose", ctx =>
        {
            Provider(ctx).Dispose();
            ctx.State.Remove(ProviderKey);
            return "XGameSaveCloseProvider";
        }, "gamesave.provider");
    }

    private static GameSaveProvider Provider(CheckContext ctx) => ctx.Get<GameSaveProvider>(ProviderKey);

    private static GameSaveContainer Container(CheckContext ctx) => ctx.Get<GameSaveContainer>(ContainerKey);
}
