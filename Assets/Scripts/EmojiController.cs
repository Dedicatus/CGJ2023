using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EmojiController : MonoBehaviour
{
    private Transform _inGameCanvas;

    public Transform InGameCanvas
    {
        get
        {
            if (_inGameCanvas == null)
            {
                _inGameCanvas = GameObject.Find("UIManager/InGameUI_WorldSpace/InGameCanvas/Emoji").transform;
                _inGameCanvas = _inGameCanvas ? _inGameCanvas : FindObjectOfType<Canvas>().transform;
            }

            return _inGameCanvas;
        }
    }

    public EmojiDisplay emojiDisplayPrefab;

    public EmojiDisplay emojiDisplay;

    public Transform body;

    [SerializeField] private EmojiDatas emojiDatas;

    public int curPriority = -1;

    [SerializeField] private float displayTime = 5.0f;

    [SerializeField, ReadOnly] private float displayTimer = 0f;

    private void Awake()
    {
        if (emojiDisplay == null)
        {
            emojiDisplay = Instantiate(emojiDisplayPrefab, InGameCanvas.transform);
            emojiDisplay.character = body;
            emojiDisplay.SetPosition();
            emojiDisplay.gameObject.SetActive(false);
        }
    }

    public void ShowEmoji(Emoji.EmojiType emojiType)
    {
        //if (emojiDatas.GetEmojiPriority(emojiType) > curPriority)
        //{
        //}
        emojiDisplay.SetSprite(emojiDatas.GetEmojiSprite(emojiType));
        //curPriority = emojiDatas.GetEmojiPriority(emojiType);
        emojiDisplay.gameObject.SetActive(true);
        displayTimer = 0f;
    }

    private void Update()
    {
        DisplayTimeHandler();
    }

    private void DisplayTimeHandler()
    {
        if (emojiDisplay.gameObject.activeSelf)
        {
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayTime)
            {
                emojiDisplay.gameObject.SetActive(false);
                displayTimer = 0f;
                curPriority = -1;
            }
        }
    }
}