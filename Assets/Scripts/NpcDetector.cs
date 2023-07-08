using System;
using System.Collections.Generic;
using System.Linq;
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
    public float uncomfortableIncrease = 0.1f;
    public float comfortableDecrease = 0.1f;
    public float uncomfortableRate;
    public float comfortableRate;
    [SerializeField] private float increaseValue;
    [SerializeField] private float decreaseValue;

    public Dictionary<int, float> speedConfigs = new();
    private List<int> _speedConfigKeys = new();

    public List<GameObject> others = new();

    public Dictionary<GameObject, float> othersDistance = new();

    [SerializeField] private HpController hpController;

    [SerializeField] private RequirementController requirementController;

    private void Awake()
    {
        othersDistance = new Dictionary<GameObject, float>();
        GetComponent<SphereCollider>().radius = lonelinessRadius * 0.5f;
        _speedConfigKeys = speedConfigs.Keys.ToList();
        _speedConfigKeys.Sort();
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

    public void Update()
    {
        var time = GameManager.Instance.GameTime;
        var speed = valueChangeSpeed;
        foreach (var key in _speedConfigKeys)
        {
            if (time >= key)
            {
                speed = speedConfigs[key];
            }
        }

        valueChangeSpeed = speed;
    }

    public void LateUpdate()
    {
        if (GameManager.Instance.GetGameState() != GameManager.GameState.RUNNING)
        {
            return;
        }

        var uncomfortableCount = 0;
        var comfortableCount = 0;
        foreach (var (other, distance) in othersDistance)
        {
            if (other && other.activeSelf)
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

        increaseValue = 0;
        decreaseValue = 0;

        if (uncomfortableCount > 0)
        {
            var increase = uncomfortableRate * uncomfortableCount;
            increaseValue = uncomfortableIncrease * valueChangeSpeed + increase;
            hpController.AddHp(increaseValue);
        }
        else if (comfortableCount > 0)
        {
            var decrease = comfortableRate * comfortableCount;
            decreaseValue = comfortableDecrease * valueChangeSpeed + decrease;
            hpController.DecHp(decreaseValue);
        }
        else
        {
            hpController.DecHp(lonelyDecrease * valueChangeSpeed);
        }
    }
}