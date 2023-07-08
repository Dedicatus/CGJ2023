using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject flagIconPrefab;
    public int slotNumber = 3;
    [ReadOnly]
    private List<GameObject> currentFlagList= new List<GameObject>();
    
    
    public RectTransform flagCanvas;
       
    // Start is called before the first frame update
    void Start()
    {
        DragableIcon.flagSpawned += replaceOneSlot;
        while (currentFlagList.Count < slotNumber)
        {
            addANewSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addANewSlot(float cd = 0)
    {
        Requirement newRequirement = (Requirement)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Requirement)).Length);
        GameObject newFlagIcon = GameObject.Instantiate(flagIconPrefab);
        newFlagIcon.GetComponent<DragableIcon>().initFlagIcon(newRequirement,cd);
        newFlagIcon.transform.SetParent(flagCanvas, false);
        currentFlagList.Add(newFlagIcon);
    }

    public void replaceOneSlot(GameObject removedSlot)
    {
        currentFlagList.Remove(removedSlot);
        Destroy(removedSlot);
        addANewSlot(10);
    }
}
