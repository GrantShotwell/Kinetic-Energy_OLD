#### [Assembly-CSharp](./Assembly-CSharp.md 'Assembly-CSharp')
### [KineticEnergy.Unity](./Assembly-CSharp.md#KineticEnergy-Unity 'KineticEnergy.Unity').[InspectorHelper](./KineticEnergy-Unity-InspectorHelper.md 'KineticEnergy.Unity.InspectorHelper')
## TargetsAreMixed&lt;ScriptType&gt;(UnityEngine.Object[], System.Func&lt;ScriptType, ScriptType, System.Boolean&gt;) `method`
Tests if any unordered pair of a property in [Editor.targets](https://docs.microsoft.com/en-us/dotnet/api/Editor.targets 'Editor.targets') are unequal.
### Type parameters

<a name='KineticEnergy-Unity-InspectorHelper-TargetsAreMixed-ScriptType-(UnityEngine-Object---_System-Func-ScriptType-_ScriptType-_System-Boolean-)-ScriptType'></a>
`ScriptType`

The [UnityEngine.Object](https://docs.microsoft.com/en-us/dotnet/api/UnityEngine.Object 'UnityEngine.Object') type that the editor is assigned to.
### Parameters

<a name='KineticEnergy-Unity-InspectorHelper-TargetsAreMixed-ScriptType-(UnityEngine-Object---_System-Func-ScriptType-_ScriptType-_System-Boolean-)-targets'></a>
`targets`

[Editor.targets](https://docs.microsoft.com/en-us/dotnet/api/Editor.targets 'Editor.targets')

<a name='KineticEnergy-Unity-InspectorHelper-TargetsAreMixed-ScriptType-(UnityEngine-Object---_System-Func-ScriptType-_ScriptType-_System-Boolean-)-mixedTest'></a>
`mixedTest`

Checks if the two given [ScriptType](https://docs.microsoft.com/en-us/dotnet/api/ScriptType 'ScriptType')s are mixed.
### Returns
Returns true if any unordered pair of the given array passes the given [Func<ScriptType, ScriptType, bool>](https://docs.microsoft.com/en-us/dotnet/api/Func<ScriptType, ScriptType, bool> 'Func<ScriptType, ScriptType, bool>').
