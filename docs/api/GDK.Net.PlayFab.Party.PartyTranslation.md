# <a id="GDK_Net_PlayFab_Party_PartyTranslation"></a> Class PartyTranslation

Namespace: [GDK.Net.PlayFab.Party](GDK.Net.PlayFab.Party.md)  
Assembly: GDK.Net.dll  

Projects <code>PARTY_TRANSLATION</code>: one machine translation of a chat message.

```csharp
public sealed class PartyTranslation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PartyTranslation](GDK.Net.PlayFab.Party.PartyTranslation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GDK_Net_PlayFab_Party_PartyTranslation_ErrorDetail"></a> ErrorDetail

The <code>PartyError</code> detail when <xref href="GDK.Net.PlayFab.Party.PartyTranslation.Result" data-throw-if-not-resolved="false"></xref> is not success.

```csharp
public uint ErrorDetail { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="GDK_Net_PlayFab_Party_PartyTranslation_LanguageCode"></a> LanguageCode

The language the text was translated into.

```csharp
public string LanguageCode { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="GDK_Net_PlayFab_Party_PartyTranslation_Options"></a> Options

Flags describing the translation, such as profanity masking.

```csharp
public PartyTranslationReceivedOptions Options { get; }
```

#### Property Value

 [PartyTranslationReceivedOptions](GDK.Net.PlayFab.Party.PartyTranslationReceivedOptions.md)

### <a id="GDK_Net_PlayFab_Party_PartyTranslation_Result"></a> Result

Whether the translation succeeded.

```csharp
public PartyStateChangeResult Result { get; }
```

#### Property Value

 [PartyStateChangeResult](GDK.Net.PlayFab.Party.PartyStateChangeResult.md)

### <a id="GDK_Net_PlayFab_Party_PartyTranslation_Translation"></a> Translation

The translated text, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when translation failed.

```csharp
public string? Translation { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

