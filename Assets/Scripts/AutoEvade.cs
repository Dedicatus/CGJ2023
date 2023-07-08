﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEvade : MonoBehaviour
{
    public float maxMoveSpd = 1f;
    public float jitterTolerance = 0.1f;
    public float evadeForceMod = 0.9f;
    public bool evadeByForce;
    private float detectRadius;
    public List<Transform> surroundingObject = new List<Transform>();
    private Vector3 randomDir;
    private Rigidbody mRigidbody;
    private FlagController targetFlag;
    public FlagController TargetFlag
    {
        get => targetFlag;
        set
        {
            if (targetFlag == null && value != null)
            {
                surroundingObject.Clear();
            }
            targetFlag = value;
        }
    }
    private void Awake()
    {
        randomDir = Random.insideUnitCircle.normalized;
        randomDir.z = randomDir.y;
        randomDir.y = 0f;
        mRigidbody = GetComponentInParent<Rigidbody>();
    }
    private void OnEnable()
    {
        detectRadius = GetComponent<SphereCollider>().radius;
    }

    private void FixedUpdate()
    {
        Evade();
    }

    private void Evade()
    {
        if (surroundingObject == null || surroundingObject.Count == 0 || maxMoveSpd <= 0f)
        {
            return;
        }
        if (targetFlag != null && targetFlag.isActiveAndEnabled)
        {
            return;
        }
        var targetPos = GetEvadePosition(surroundingObject);
        if (evadeByForce)
        {
            mRigidbody.AddForce((targetPos - transform.parent.position) * maxMoveSpd);
        }
        else
        {
            var inJitterToleranceDist = Vector3.Distance(targetPos, transform.position) <= jitterTolerance;
            if (!inJitterToleranceDist)
            {
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPos, maxMoveSpd * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (targetFlag != null && collision.GetComponent<AutoEvade>().targetFlag == targetFlag)
        {
            surroundingObject.Add(collision.transform);
        }
        else if (targetFlag == null)
        {
            surroundingObject.Add(collision.transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        surroundingObject.Remove(collision.transform);
    }

    private Vector3 GetEvadePosition(List<Transform> transforms)//!!O(n^2) algorithm!
    {
        if (transforms.Count == 0)
        {
            return transform.parent.position;
        }

        var avoidCrowdedPos = transform.parent.position;
        var centerPos = Vector3.zero;
        foreach (var trans in transforms)
        {
            var deltaAvoidBias = (transform.position - trans.position) / transforms.Count;
            var dist = Vector3.Distance(trans.position, transform.position);
            //var hwRatio = Mathf.Abs(Vector3.Dot((trans.position - transform.position).normalized, Vector3.right));
            //hwRatio *= 0.8f;
            //hwRatio += 0.2f;
            deltaAvoidBias *= evadeForceMod / Mathf.Pow(Mathf.Max(dist, 0.01f), 2f);
            deltaAvoidBias *= Mathf.Lerp(1f, 0f, dist / (detectRadius * 2f));
            //deltaAvoidBias *= Mathf.Lerp(1f, 2.5f, hwRatio);//avoid offset bigger on horizontal than virtical//use for hp slider
            avoidCrowdedPos += deltaAvoidBias;
            centerPos += (trans.position / transforms.Count) * Mathf.Lerp(1f, 0.0001f, Vector3.Distance(centerPos, transform.position) / (detectRadius * 2f));
        }

        return Vector3.Scale(avoidCrowdedPos, new Vector3(1f, 0f, 1f));
    }
}