using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float moveToFlagSpeed;
    private FlagController targetFlag;
    void Start()
    {
        FlagController.OnSetFlag += OnSetFlag;
    }

    private void OnSetFlag(FlagController flagCtrl)
    {
        if (flagCtrl.requirement == GetComponent<RequirementController>().GetFirstRequirement())
        {
            GetComponentInChildren<AutoEvade>().TargetFlag = flagCtrl;
            targetFlag = flagCtrl;
        }
        else if (flagCtrl.requirement == GetComponent<RequirementController>().GetSecondRequirement())
        {
            GetComponentInChildren<AutoEvade>().TargetFlag = flagCtrl;
            targetFlag = flagCtrl;
        }
    }

    void Update()
    {
        MoveToFlagFormationRange();
    }
    private void MoveToFlagFormationRange()
    {
        if (targetFlag == null)
        {
            return;
        }
        var inFormationRange = targetFlag.InFormationRange(transform.position);
        var inAttarctRange = targetFlag.InAttarctRange(transform.position);
        if ( targetFlag.isActiveAndEnabled && inAttarctRange && !inFormationRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFlag.transform.position, moveToFlagSpeed * Time.deltaTime);
        }
    }
}
