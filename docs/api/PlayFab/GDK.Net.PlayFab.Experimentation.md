# <a id="GDK_Net_PlayFab_Experimentation"></a> Class Experimentation

Namespace: [GDK.Net.PlayFab](GDK.Net.PlayFab.md)  
Assembly: GDK.Net.dll  

The PlayFab Experimentation service (<code>PFExperimentation.h</code>).

```csharp
public static class Experimentation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Experimentation](GDK.Net.PlayFab.Experimentation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GDK_Net_PlayFab_Experimentation_GetTreatmentAssignmentAsync_GDK_Net_PlayFab_PlayFabEntity_GDK_Net_PlayFab_ExperimentationGetTreatmentAssignmentRequest_System_Threading_CancellationToken_"></a> GetTreatmentAssignmentAsync\(PlayFabEntity, ExperimentationGetTreatmentAssignmentRequest, CancellationToken\)

Calls <code>PFExperimentationGetTreatmentAssignmentAsync</code>.

```csharp
public static Task<ExperimentationGetTreatmentAssignmentResult> GetTreatmentAssignmentAsync(PlayFabEntity entity, ExperimentationGetTreatmentAssignmentRequest request, CancellationToken cancellationToken = default)
```

#### Parameters

`entity` [PlayFabEntity](GDK.Net.PlayFab.PlayFabEntity.md)

`request` [ExperimentationGetTreatmentAssignmentRequest](GDK.Net.PlayFab.ExperimentationGetTreatmentAssignmentRequest.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[ExperimentationGetTreatmentAssignmentResult](GDK.Net.PlayFab.ExperimentationGetTreatmentAssignmentResult.md)\>

