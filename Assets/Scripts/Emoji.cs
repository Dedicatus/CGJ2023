using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emoji
{
    public enum EmojiType
    {
        CRY = 0,
        UPSET = 15,
        HAPPY = 50,
        RELAX = 60,
        TIRED = 85,
        OVERLOADED = 100
    }
    public EmojiType emojiType;
    public int priority;
    public Sprite sprite;
}