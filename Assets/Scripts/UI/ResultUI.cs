using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultUI : MonoSingleton<ResultUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    [SerializeField] private TMP_Text scoreText;

    private void Start() 
    {
        myCanvas = transform.GetChild(0).gameObject;
    }

    private void OnEnable() 
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameEnd += SetScoreText;
    }

    public void OnReturnButtonClicked()
    {
        gameManager.ReloadCurrentScene();
    }

    private void SetScoreText()
    {
        string inGameTimeString = string.Format("{0:00}:{1:00}", gameManager.InGameHours, gameManager.InGameMinutes);
        scoreText.text = inGameTimeString;
    }
}
