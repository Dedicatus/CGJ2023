using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameManager gameManager;

    private MainMenuUI mainMenuUI;
    private PauseUI pauseUI;
    private ResultUI resultUI;
    private InGameUI inGameUI;
    private InGameUIWorldSpace inGameUIWorldSpace;

    private void OnEnable() 
    {
        gameManager = GameManager.Instance;
        //gameManager.OnPregameEnter += OnPregameEnter;
        gameManager.OnGameStart += OnGameStart;
        gameManager.OnGamePause += OnGamePause;
        gameManager.OnGameEnd += OnGameEnd;
    }

    private void Start() 
    {
        mainMenuUI = MainMenuUI.Instance;
        pauseUI = PauseUI.Instance;
        resultUI = ResultUI.Instance;
        inGameUI = InGameUI.Instance;
        inGameUIWorldSpace = InGameUIWorldSpace.Instance;
        SwitchUI(gameManager.CurGameState);
    }

    private void OnDisable() 
    {
        //gameManager.OnPregameEnter -= OnPregameEnter;
        gameManager.OnGameStart -= OnGameStart;
        gameManager.OnGamePause -= OnGamePause;
        gameManager.OnGameEnd -= OnGameEnd;
    }

    private void OnPregameEnter()
    {
        SwitchUI(GameManager.GameState.PREGAME);
    }

    private void OnGameStart()
    {
        SwitchUI(GameManager.GameState.RUNNING);
    }

    private void OnGamePause()
    {
        SwitchUI(GameManager.GameState.PAUSED);
    }

    private void OnGameEnd()
    {
        SwitchUI(GameManager.GameState.RESULT);
    }

    public void SwitchUI(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.PREGAME:
                mainMenuUI.myCanvas.SetActive(true);
                pauseUI.myCanvas.SetActive(false);
                resultUI.myCanvas.SetActive(false);
                inGameUI.myCanvas.SetActive(false);
                inGameUIWorldSpace.myCanvas.SetActive(false);
                break;
            case GameManager.GameState.RUNNING:
                mainMenuUI.myCanvas.SetActive(false);
                pauseUI.myCanvas.SetActive(false);
                resultUI.myCanvas.SetActive(false);
                inGameUI.myCanvas.SetActive(true);
                inGameUIWorldSpace.myCanvas.SetActive(true);
                break;
            case GameManager.GameState.PAUSED:
                mainMenuUI.myCanvas.SetActive(false);
                pauseUI.myCanvas.SetActive(true);
                resultUI.myCanvas.SetActive(false);
                inGameUI.myCanvas.SetActive(true);
                inGameUIWorldSpace.myCanvas.SetActive(true);
                break;
            case GameManager.GameState.RESULT:
                mainMenuUI.myCanvas.SetActive(false);
                pauseUI.myCanvas.SetActive(false);
                resultUI.myCanvas.SetActive(true);
                inGameUI.myCanvas.SetActive(false);
                inGameUIWorldSpace.myCanvas.SetActive(false);
                break;
            default:
                break;
        }
    }
}
