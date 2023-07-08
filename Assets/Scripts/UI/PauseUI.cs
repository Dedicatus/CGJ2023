using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoSingleton<PauseUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        myCanvas = transform.GetChild(0).gameObject;
    }

    public void OnResumeButtonClicked()
    {
        gameManager.SetGameState(GameManager.GameState.RUNNING);
    }
}
