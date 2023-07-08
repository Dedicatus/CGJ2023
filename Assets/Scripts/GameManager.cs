using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class GameManager : MonoSingleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        GAMEEND
    }

    [SerializeField] private GameState curGameState = GameState.PREGAME;

    public UnityAction OnPregameEnter;

    public UnityAction OnGameStart;

    public UnityAction OnGamePause;

    public UnityAction OnGameEnd;

    private float gameTime = 0f;
    public float GameTime
    {
        get
        {
            return gameTime;
        }
    }

    [SerializeField] private float realSecondsToInGameHour = 1.0f;

    [SerializeField] private int startingHour = 20;

    [SerializeField, ReadOnly] private int inGameHours = 0;
    public int InGameHours
    {
        get
        {
            return inGameHours;
        }
    }

    [SerializeField, ReadOnly] private int inGameMinutes = 0;
    public int InGameMinutes
    {
        get
        {
            return inGameMinutes;
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
            CalculateScoreText();
        }
    }

    private void CalculateScoreText()
    {
        float elapsedHours = gameTime / realSecondsToInGameHour;
        int tempInGameHours = (int)elapsedHours % 24;
        inGameMinutes = (int)(elapsedHours * 60) % 60;

        if (tempInGameHours + startingHour >= 24)
        {
            tempInGameHours -= 24;
        }
        inGameHours = tempInGameHours + startingHour;
    }
}
