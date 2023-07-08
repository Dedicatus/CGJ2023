using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public GameObject flagIconPrefab;
    public int slotNumber = 3;
    [ShowInInspector,ReadOnly]
    private List<GameObject> currentFlagList= new List<GameObject>();
    [ShowInInspector, ReadOnly]
    private Dictionary<Requirement, int> usedFlagDict = new Dictionary<Requirement, int>();
    private int usedFlagNumber = 0;
    
    public RectTransform flagCanvas;
       
    // Start is called before the first frame update
    void Start()
    {
        foreach (Requirement requirement in Enum.GetValues(typeof(Requirement)))
        {
            if (requirement != Requirement.none)
            {
                usedFlagDict[requirement] = 0;
            }
        }
        DragableIcon.flagSpawned += replaceOneSlot;
        while (currentFlagList.Count < slotNumber)
        {
            addANewSlot();
        }

        //for (int i = 0; i < 1000; i++)
        //{
        //    getNewRequirement();
        //}
    }

    private void OnDisable()
    {
        DragableIcon.flagSpawned -= replaceOneSlot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addANewSlot(float cd = 0)
    {
        Requirement newRequirement = getNewRequirement();
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

    private Requirement getNewRequirement()
    {
        Requirement newRequirement = Requirement.none;
        Dictionary<Requirement, float> weightList = new Dictionary<Requirement, float>();
        float totalWeight = 0; 
        foreach(var(requirement,realCount) in usedFlagDict)
        {
            if (requirement != Requirement.none)
            {
                int expectationCount = usedFlagNumber/Enum.GetValues(typeof(Requirement)).Length;
                if (expectationCount - realCount > 2)
                {
                    Debug.Log("Maybe!");
                }
                float weight = 3/(3.001f-(expectationCount - realCount));
                if (weight < 0)
                {
                    weight = 10000;
                }
                totalWeight += weight;
                weightList.Add(requirement, totalWeight);
            }
        }
        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float lowerValue = 0;
        foreach(var(requirement, requiredWeight) in weightList)
        {
            if (randomValue > lowerValue && randomValue < requiredWeight)
            {
                newRequirement = requirement;
                break;
            }
            else
            {
                lowerValue = requiredWeight;
                continue;
            }
        }

        usedFlagDict[newRequirement] = usedFlagDict[newRequirement]+1;
        
        usedFlagNumber++;
        return newRequirement;
    }
}
