using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoSingleton<MainMenuUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        myCanvas = transform.GetChild(0).gameObject;
    }

    public void OnPlayButtonClicked()
    {
        gameManager.SetGameState(GameManager.GameState.RUNNING);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

}
