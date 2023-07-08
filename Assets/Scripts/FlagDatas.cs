using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FlagDatas", menuName = "ScriptableObjects/FlagDatas", order = 1)]
public class FlagDatas : ScriptableObject
{
    public List<RequirementSprite> flagRequirements;

    public Sprite GetFlagSprite(Requirement requirement)
    {
        RequirementSprite requirementSprite = flagRequirements.Find(x => x.requirementType == requirement);
        return requirementSprite.flagSprite;
    }
    public Sprite GetIconSprite(Requirement requirement)
    {
        RequirementSprite requirementSprite = flagRequirements.Find(x => x.requirementType == requirement);
        return requirementSprite.iconSprite;
    }

    public Color GetColor(Requirement requirement)
    {
        RequirementSprite requirementSprite = flagRequirements.Find(x => x.requirementType == requirement);
        return requirementSprite.color;
    }
}