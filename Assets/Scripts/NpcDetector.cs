using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

// [RequireComponent(typeof(HpController), typeof(RequirementController))]
public class NpcDetector : SerializedMonoBehaviour
{
    public float uncomfortableRadius = 5f;
    public float lonelinessRadius = 15;
    public float valueChangeSpeed = 1;
    public float lonelyDecrease = 0.1f;
    public float uncomfortableDecrease = 0.1f;
    public float comfortableIncrease = 0.1f;
    public float uncomfortableRate;
    public float comfortableRate;

    public List<GameObject> others = new();

    public Dictionary<GameObject, float> othersDistance = new Dictionary<GameObject, float>();

    [SerializeField] private HpController hpController;

    [SerializeField] private RequirementController requirementController;

    private void Awake()
    {
        othersDistance = new Dictionary<GameObject, float>();
        GetComponent<SphereCollider>().radius = lonelinessRadius * 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Npc"))
        {
            return;
        }

        if (!others.Contains(other.gameObject))
        {
            others.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        othersDistance.Clear();
        others.RemoveAll(t => t == null || t.gameObject == null);
        foreach (var otherNpcDetector in others)
        {
            var distance = Vector3.Distance(transform.position, otherNpcDetector.transform.position);
            othersDistance.Add(otherNpcDetector, distance);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Npc"))
        {
            return;
        }

        if (others.Contains(other.gameObject))
        {
            others.Remove(other.gameObject);
        }
    }

    public void LateUpdate()
    {
        var uncomfortableCount = 0;
        var comfortableCount = 0;
        foreach (var (other, distance) in othersDistance)
        {
            if (other.gameObject.activeSelf)
            {
                if (distance <= uncomfortableRadius)
                {
                    uncomfortableCount++;
                }
                else if (distance <= lonelinessRadius)
                {
                    comfortableCount++;
                }
            }
        }

        if (uncomfortableCount > 0)
        {
            var decrease = uncomfortableRate * uncomfortableCount;
            hpController.DecHp(uncomfortableDecrease * valueChangeSpeed + decrease);
        }
        else if (comfortableCount > 0)
        {
            var increase = comfortableRate * comfortableCount;
            hpController.AddHp(comfortableIncrease * valueChangeSpeed + increase);
        }
        else
        {
            hpController.DecHp(lonelyDecrease * valueChangeSpeed);
        }
    }
}