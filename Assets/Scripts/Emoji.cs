using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emoji
{
    public enum EmojiType
    {
        GOOD,
        BAD
    }
    public EmojiType emojiType;
    public int priority;
    public Sprite sprite;
}