using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(RequirementColor), menuName = nameof(RequirementColor), order = 0)]
public class RequirementColor : SerializedScriptableObject
{
    public Dictionary<Requirement, Color> Colors;
    public Dictionary<Requirement, Material> Materials;

    public Color GetColor(Requirement requirement)
    {
        return Colors.TryGetValue(requirement, out var color) ? color : Color.white;
    }

    public Material GetMaterial(Requirement requirement)
    {
        return Materials.TryGetValue(requirement, out var color) ? color : null;
    }
}