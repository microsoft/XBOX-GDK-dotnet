# Title-implemented UI

How a title takes over the Gaming Runtime's dialogs and draws them itself, and the threading rules
that come with doing so.

By default the Gaming Runtime draws its own dialogs. `CustomGameUi.SetHandlers` registers the title
as the renderer instead, so `GameUiManager.ShowMessageDialogAsync` and its siblings invoke the
title's handler with a request object rather than showing system UI; the title draws whatever it
likes and calls `Respond` to complete the original operation. Handlers are per-UI and optional, so a
title can take over just the dialogs it cares about. This is how a title keeps a consistent visual
style, and on platforms with no system UI it is the only way these APIs work at all.

```csharp
CustomGameUi.SetHandlers(new CustomGameUiHandlers
{
    MessageDialog = request =>
    {
        // Runs on a thread pool thread, not the runtime's callback thread — blocking and awaiting
        // are both fine here. Responding may also happen later, from any thread.
        myGame.ShowDialog(request.TitleText, request.ContentText, chosen => request.Respond(chosen));
    },
});
```

**Threading.** The runtime raises these callbacks on the work port of the task queue driving the
originating operation, and it expects the title to get off that port rather than work on it. So the
projection copies the request payload out of runtime-owned memory, hands the request to the thread
pool, and returns from the native callback immediately. A handler may therefore block, `await`, and
in particular await further Gaming Runtime operations on that same queue without deadlocking against
itself. The cost is that handlers arrive with no synchronization context, so a title that must touch
its renderer marshals to its own thread as it would for any other background callback. The borrowed
`XTaskQueueHandle` is deliberately not exposed on the request: it would dangle the moment the
callback returns, and it is the one queue a handler must not schedule onto. A handler that throws
cannot take the process down, and if it throws before responding the projection posts the neutral
response on its behalf so the caller's operation is never left pending.

**Unregistering.** `CustomGameUi.ClearHandlers` passes a table whose function pointers are all null,
not a null pointer. `XGameUiSetUiCallbacks` dereferences its argument unconditionally, so passing
`nullptr` access-violates inside the Gaming Runtime and takes the process down — worth knowing for
the other language projections, since nothing in the header says so.
