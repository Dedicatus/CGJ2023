using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EmojiDatas", menuName = "ScriptableObjects/EmojiData", order = 1)]
public class EmojiDatas : ScriptableObject
{
    public List<Emoji> emojiDataList;

    public Sprite GetEmojiSprite(Emoji.EmojiType emojiType)
    {
        Emoji emoji = emojiDataList.Find(x => x.emojiType == emojiType);
        return emoji.sprite;
    }

    public int GetEmojiPriority(Emoji.EmojiType emojiType)
    {
        Emoji emoji = emojiDataList.Find(x => x.emojiType == emojiType);
        return emoji.priority;
    }
}