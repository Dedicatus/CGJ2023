using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    private Canvas _inGameCanvas;

    public Canvas InGameCanvas
    {
        get
        {
            if (_inGameCanvas == null)
            {
                _inGameCanvas = GameObject.Find("UIManager/InGameUI_WorldSpace/InGameCanvas").GetComponent<Canvas>();
                _inGameCanvas = _inGameCanvas ? _inGameCanvas : FindObjectOfType<Canvas>();
            }

            return _inGameCanvas;
        }
    }

    public EmojiDisplay emojiDisplayPrefab;

    public EmojiDisplay emojiDisplay;

    private void Awake()
    {
        if (emojiDisplay == null)
        {
            emojiDisplay = Instantiate(emojiDisplayPrefab, InGameCanvas.transform);
            emojiDisplay.character = transform;
        }
    }
}
