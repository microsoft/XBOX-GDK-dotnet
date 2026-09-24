# <a id="GDK_Net_Activation_GameActivationType"></a> Enum GameActivationType

Namespace: [GDK.Net.Activation](GDK.Net.Activation.md)  
Assembly: GDK.Net.dll  

How the title was activated.

```csharp
public enum GameActivationType
```

## Fields

`AcceptedGameInvite = 4` 

An invite the user accepted while the title was already running
(<code>XGameActivationType::AcceptedGameInvite</code>).



`File = 2` 

Activated by launching an associated file type
(<code>XGameActivationType::File</code>). The URI is the file path.

Only reported by <xref href="GDK.Net.Activation.GameActivationManager.Activated" data-throw-if-not-resolved="false"></xref>, which is backed by the
unified <code>XGameActivationRegisterForEvent</code>.

`Invite = 1` 

A multiplayer game invite (<code>XGameInviteRegisterForEvent</code>). The URI is the invite handle
to hand to the multiplayer service.



`PendingGameInvite = 3` 

An invite the user accepted while the title was not running, or accepted from outside it
(<code>XGameActivationType::PendingGameInvite</code>).

A pending invite is replayed on registration rather than raised live, and is only consumed
once the title calls <xref href="GDK.Net.Activation.GameActivationManager.AcceptPendingInvite(System.String)" data-throw-if-not-resolved="false"></xref>.

`Protocol = 0` 

Activated through a registered protocol URI (<code>XGameProtocolRegisterForActivation</code>).



