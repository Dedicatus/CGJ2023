using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIWorldSpace : MonoSingleton<InGameUIWorldSpace>
{
    private GameManager gameManager;
    
    public GameObject myCanvas;

    private void Start() 
    {
        gameManager = GameManager.Instance;
        myCanvas = transform.GetChild(0).gameObject;
    }
}