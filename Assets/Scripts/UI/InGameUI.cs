using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUI : MonoSingleton<InGameUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    [SerializeField] private TMP_Text inGameTimeText;

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject returnButton;

    private int lastInGameHours = 0;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        myCanvas = transform.GetChild(0).gameObject;
        SwitchButton(false);
        lastInGameHours = gameManager.InGameHours;
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
        if (lastInGameHours != gameManager.InGameHours)
        {
            //Shake
            lastInGameHours = gameManager.InGameHours;
        }
        SetScoreText();
    }

    public void OnReturnButtonClicked()
    {
        gameManager.ReloadCurrentScene();
    }

    public void SwitchButton(bool isPaused)
    {
        pauseButton.SetActive(!isPaused);
        returnButton.SetActive(isPaused);
    }
}
