using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoSingleton<InGameUI>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

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

    }

    private void Update() 
    {
        SetScoreText();
    }
}
