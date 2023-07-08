using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RequirementColor), menuName = nameof(RequirementColor), order = 0)]
public class RequirementColor : SerializedScriptableObject
{
    public Dictionary<Requirement, Color> Colors;
}