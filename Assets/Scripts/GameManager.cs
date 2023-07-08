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
        RESULT
    }

    [SerializeField] private GameState curGameState = GameState.PREGAME;

    public GameState CurGameState
    {
        get
        {
            return curGameState;
        }
    }

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

    public HashSet<GameObject> npcHashSet;

    private void Start() 
    {
        npcHashSet = new HashSet<GameObject>();
        SetGameState(curGameState);
    }

    public void RegisterNPC(GameObject npc)
    {
        npcHashSet.Add(npc);
    }

    public void UnregisterNPC(GameObject npc)
    {
        npcHashSet.Remove(npc);

        if (npcHashSet.Count <= 0)
        {
            SetGameState(GameState.RESULT);
        }
    }

    public void SetGameState(GameState state)
    {
        curGameState = state;
        switch (curGameState)
        {
            case GameState.PREGAME:
                OnPregameEnter?.Invoke();
                Time.timeScale = 0f;
                gameTime = 0f;
                break;
            case GameState.RUNNING:
                OnGameStart?.Invoke();
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSED:
                OnGamePause?.Invoke();
                Time.timeScale = 0f;
                break;
            case GameState.RESULT:
                OnGameEnd?.Invoke();
                Time.timeScale = 0f;
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

    // private void ReloadCurrentScene()
    // {
    //     // Get the index of the current active scene
    //     int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    //     // Load the scene with the same index, effectively reloading it
    //     SceneManager.LoadScene(currentSceneIndex);
    // }
}
