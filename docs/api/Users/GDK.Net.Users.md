# <a id="GDK_Net_Users"></a> Namespace GDK.Net.Users

### Classes

 [RemoteConnectClosePromptEventArgs](GDK.Net.Users.RemoteConnectClosePromptEventArgs.md)

The Gaming Runtime has finished with a remote-connect prompt and the title should take it down.

 [RemoteConnectShowPromptEventArgs](GDK.Net.Users.RemoteConnectShowPromptEventArgs.md)

The Gaming Runtime is asking the title to display a remote-connect prompt: the user should visit
<xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Url" data-throw-if-not-resolved="false"></xref> on a second device and enter <xref href="GDK.Net.Users.RemoteConnectShowPromptEventArgs.Code" data-throw-if-not-resolved="false"></xref> to finish signing in.

 [SignOutDeferral](GDK.Net.Users.SignOutDeferral.md)

A sign-out deferral that prevents the Gaming Runtime from completing a user sign-out until the
deferral is disposed. Wraps <code>XUserSignOutDeferralHandle</code>.

 [SpopPromptEventArgs](GDK.Net.Users.SpopPromptEventArgs.md)

The Gaming Runtime is asking the title to display a "signed in on another device" (SPOP) prompt
and report back what the user chose.

 [TokenAndSignature](GDK.Net.Users.TokenAndSignature.md)

The Xbox Live token and optional body-signature returned by
<code>XUserGetTokenAndSignature[Utf16]Async</code>.

 [User](GDK.Net.Users.User.md)

A signed-in user. Wraps <code>XUserHandle</code>.

 [UserChangedEventArgs](GDK.Net.Users.UserChangedEventArgs.md)

Payload for <xref href="GDK.Net.Users.UserManager.UserChanged" data-throw-if-not-resolved="false"></xref>.

 [UserDefaultAudioEndpointChangedEventArgs](GDK.Net.Users.UserDefaultAudioEndpointChangedEventArgs.md)

Payload for <xref href="GDK.Net.Users.UserManager.DefaultAudioEndpointChanged" data-throw-if-not-resolved="false"></xref>.

 [UserDeviceAssociationChangedEventArgs](GDK.Net.Users.UserDeviceAssociationChangedEventArgs.md)

Payload for <xref href="GDK.Net.Users.UserManager.DeviceAssociationChanged" data-throw-if-not-resolved="false"></xref>.

 [UserManager](GDK.Net.Users.UserManager.md)

User sign-in and change notifications. Reached through <xref href="GDK.Net.GameRuntime.Users" data-throw-if-not-resolved="false"></xref>.

 [UserPlatform](GDK.Net.Users.UserPlatform.md)

The <code>XUserPlatform</code> family: lets the title draw the Gaming Runtime's sign-in prompts itself
instead of using the system UI: the remote-connect prompt ("open this URL on another device")
and the SPOP prompt ("this account is already signed in somewhere else").

### Structs

 [AppLocalDeviceId](GDK.Net.Users.AppLocalDeviceId.md)

A 32-byte opaque device identifier. Mirrors <code>APP_LOCAL_DEVICE_ID</code> from windef.h.

 [TokenAndSignatureHttpHeader](GDK.Net.Users.TokenAndSignatureHttpHeader.md)

A single HTTP header for a token-and-signature request.

 [UserLocalId](GDK.Net.Users.UserLocalId.md)

A machine-stable identifier for a signed-in user. Mirrors <code>XUserLocalId</code>.

### Enums

 [GamerPictureSize](GDK.Net.Users.GamerPictureSize.md)

Mirrors <code>XUserGamerPictureSize</code>.

 [GamertagComponent](GDK.Net.Users.GamertagComponent.md)

Which part of the gamertag to read. Mirrors <code>XUserGamertagComponent</code>.

 [SpopOperationResult](GDK.Net.Users.SpopOperationResult.md)

How the title resolved a "signed in on another device" (SPOP) prompt. Mirrors
<code>XUserPlatformSpopOperationResult</code>; passed to <xref href="GDK.Net.Users.SpopPromptEventArgs.Complete(GDK.Net.Users.SpopOperationResult)" data-throw-if-not-resolved="false"></xref>.

 [TokenAndSignatureOptions](GDK.Net.Users.TokenAndSignatureOptions.md)

Options for token-and-signature requests. Mirrors <code>XUserGetTokenAndSignatureOptions</code>.

 [UserAddOptions](GDK.Net.Users.UserAddOptions.md)

Options for <xref href="GDK.Net.Users.UserManager.AddAsync(GDK.Net.Users.UserAddOptions%2cSystem.Threading.CancellationToken)" data-throw-if-not-resolved="false"></xref>. Mirrors <code>XUserAddOptions</code>.

 [UserAgeGroup](GDK.Net.Users.UserAgeGroup.md)

Mirrors <code>XUserAgeGroup</code>.

 [UserChangeEvent](GDK.Net.Users.UserChangeEvent.md)

Mirrors <code>XUserChangeEvent</code>.

 [UserDefaultAudioEndpointKind](GDK.Net.Users.UserDefaultAudioEndpointKind.md)

The audio endpoint role. Mirrors <code>XUserDefaultAudioEndpointKind</code> from XUser.h.

 [UserPrivilege](GDK.Net.Users.UserPrivilege.md)

Mirrors <code>XUserPrivilege</code>.

 [UserPrivilegeDenyReason](GDK.Net.Users.UserPrivilegeDenyReason.md)

Mirrors <code>XUserPrivilegeDenyReason</code>.

 [UserPrivilegeOptions](GDK.Net.Users.UserPrivilegeOptions.md)

Mirrors <code>XUserPrivilegeOptions</code>.

 [UserState](GDK.Net.Users.UserState.md)

Mirrors <code>XUserState</code>.

