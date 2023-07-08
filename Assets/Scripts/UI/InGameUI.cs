using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoSingleton<InGameUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    [SerializeField] private TMP_Text inGameTimeText;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        myCanvas = transform.GetChild(0).gameObject;
    }

    public void OnPauseButtonClicked()
    {
        gameManager.SetGameState(GameManager.GameState.PAUSED);
    }

    private void SetScoreText()
    {
        string inGameTimeString = string.Format("{0:00}:{1:00}", gameManager.InGameHours, gameManager.InGameMinutes);
        inGameTimeText.text = inGameTimeString;
    }

    private void Update() 
    {
        SetScoreText();
    }
}
