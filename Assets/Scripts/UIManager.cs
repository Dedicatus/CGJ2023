using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameManager gameManager;

    private MainMenuUI mainMenuUI;

    private void OnEnable() 
    {
        gameManager = GameManager.Instance;
        gameManager.OnPregameEnter += OnPregameEnter;
        gameManager.OnGameStart += OnGameStart;
        gameManager.OnGamePause += OnGamePause;
    }

    private void Start() 
    {
        mainMenuUI = MainMenuUI.Instance;
    }

    private void OnDisable() 
    {
        gameManager.OnPregameEnter -= OnPregameEnter;
        gameManager.OnGameStart -= OnGameStart;
        gameManager.OnGamePause -= OnGamePause;
    }

    private void OnPregameEnter()
    {
        mainMenuUI.myCanvas.SetActive(true);
    }

    private void OnGameStart()
    {
        mainMenuUI.myCanvas.SetActive(false);
    }

    private void OnGamePause()
    {
        mainMenuUI.myCanvas.SetActive(true);
    }
}
