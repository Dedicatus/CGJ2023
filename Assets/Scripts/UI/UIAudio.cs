using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    private AudioManager audioManager;

    private void Start() 
    {
        audioManager = AudioManager.Instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       audioManager.PlaySound("UIHover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.PlaySound("UIClick");
    }
}
