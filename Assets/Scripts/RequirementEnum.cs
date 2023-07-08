using System;
using UnityEngine;

public enum Requirement
{
    none = 0,
    // Food,
    // Drinks,
    // BodyTouch,
    // Alone,
    // Peace,
    // Play,
    // Music,
    // Art,
    // Sculpture,
    //

    Red,
    Blue,
    Yellow,
    Green,
    Purple,
    // Orange,
}
[Serializable]
public class RequirementSprite
{
    public Requirement requirementType;
    public Sprite iconSprite;
    public Sprite flagSprite;
    public Color color;
}