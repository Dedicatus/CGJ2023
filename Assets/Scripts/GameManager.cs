using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    [SerializeField] private GameState curGameState = GameState.PREGAME;

    public UnityAction OnPregameEnter;

    public UnityAction OnGameStart;

    public UnityAction OnGamePause;

    private float gameTime = 0f;
    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

    public void SetGameState(GameState state)
    {
        curGameState = state;
        switch (curGameState)
        {
            case GameState.PREGAME:
                OnPregameEnter?.Invoke();
                Time.timeScale = 1;
                gameTime = 0f;
                break;
            case GameState.RUNNING:
                OnGameStart?.Invoke();
                Time.timeScale = 1;
                break;
            case GameState.PAUSED:
                OnGamePause?.Invoke();
                Time.timeScale = 0;
                break;
        }
    }

    public GameState GetGameState()
    {
        return curGameState;
    }

    private void Update() 
    {
        if (curGameState == GameState.RUNNING)
        {
            gameTime += Time.deltaTime;
        }
    }
}
