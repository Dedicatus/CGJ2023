using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float moveToFlagSpeed;
    private FlagController targetFlag;
    private Vector3 targetPos;
    void Start()
    {
        FlagController.OnSetFlag += OnSetFlag;
    }

    private void OnSetFlag(FlagController flagCtrl)
    {
        //if (flagCtrl != null && flagCtrl.isActiveAndEnabled)
        //{
        //    return;
        //}
        if (flagCtrl.requirement == GetComponent<RequirementController>().requirement)
        {
            GetComponentInChildren<AutoEvade>().TargetFlag = flagCtrl;
            targetFlag = flagCtrl;
        }
        //else if (flagCtrl.requirement == GetComponent<RequirementController>().GetSecondRequirement())
        //{
        //    GetComponentInChildren<AutoEvade>().TargetFlag = flagCtrl;
        //    targetFlag = flagCtrl;
        //}
    }

    void Update()
    {
        MoveToFormationPosition();
    }
    private void MoveToFormationPosition()
    {
        if (targetFlag == null || !targetFlag.isActiveAndEnabled)
        {
            targetPos = Vector3.zero;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveToFlagSpeed * Time.deltaTime);
    }
    public void SetTargetPosition(Vector3 pos)
    {
        targetPos = Vector3.Scale(pos, new Vector3(1f, 0f, 1f));
    }
}
