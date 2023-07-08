using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject fakeflagPrefab;
    public GameObject flagPrefab;
    [ShowInInspector,ReadOnly]
    private Requirement requirement;
    [ShowInInspector,ReadOnly]
    private float currentcd = 10;
    private float maxCD = 10;
    private bool canSpawn = true;
    private GameObject myFakeFlag;
    public static UnityAction<GameObject> flagSpawned;
    public Image flagImage;
    public Image coverImage;

    public void initFlagIcon(Requirement require, float newcd = 0)
    {
        requirement = require;
        switch (requirement)
        {
            case Requirement.Blue:
                flagImage.color = Color.blue;
                break;
            case Requirement.Gray:
                flagImage.color = Color.gray;
                break;
            case Requirement.Dark:
                flagImage.color = Color.black;
                break;
            case Requirement.Green:
                flagImage.color = Color.green;
                break;
            case Requirement.Red:
                flagImage.color = Color.red;
                break;
            case Requirement.Yellow:
                flagImage.color = Color.yellow;
                break;
            case Requirement.none:
                flagImage.color = Color.white;
                break;
        }
        maxCD = newcd;
        currentcd = newcd;
        canSpawn = newcd > 0 ? false : true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            // Store the initial position of the object and the mouse
            myFakeFlag = Instantiate(fakeflagPrefab);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            LayerMask groundLayer = 1 << LayerMask.NameToLayer("Ground");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // The raycast will only hit objects in the Ground layer
                myFakeFlag.transform.position = hit.point;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            LayerMask groundLayer = 1 << LayerMask.NameToLayer("Ground");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Destroy(myFakeFlag.gameObject);
                Instantiate(flagPrefab, hit.point, Quaternion.identity);
                flagSpawned.Invoke(gameObject);
            }
            else
            {
                Debug.Log("InvalidLoc");
                Destroy(myFakeFlag.gameObject); 
            }
        }
    }

    private void Update()
    {
        if (currentcd > 0)
        {
            currentcd -= Time.deltaTime;
            coverImage.fillAmount = currentcd / maxCD;
            if (currentcd <= 0)
            {
                canSpawn = true;
            }
        }
    }
}
