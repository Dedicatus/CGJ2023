using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoSingleton<InGameUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    [SerializeField] private TMP_Text inGameTimeText;

    [SerializeField] private float realSecondsToInGameHour = 1.0f;

    [SerializeField] private int startingHour = 20;

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
        float inGameHours = gameManager.GameTime / realSecondsToInGameHour;
        int inGameHoursInt = (int)inGameHours % 24;
        int inGameMinutesInt = (int)(inGameHours * 60) % 60;

        if (inGameHoursInt + startingHour >= 24)
        {
            inGameHoursInt -= 24;
        }

        string inGameTimeString = string.Format("{0:00}:{1:00}", inGameHoursInt + startingHour, inGameMinutesInt);
        inGameTimeText.text = inGameTimeString;
    }

    private void Update() 
    {
        SetScoreText();
    }
}
