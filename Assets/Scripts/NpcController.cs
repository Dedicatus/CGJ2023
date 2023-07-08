using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float moveToFlagSpeed;
    private FlagController targetFlag;
    private GameManager gameManager;
    private HpController hpController;

    private void OnEnable() 
    {
        hpController = GetComponent<HpController>();
        hpController.OnDie += OnDie;
        FlagController.OnSetFlag += OnSetFlag;
    }

    private void OnDisable() 
    {
        FlagController.OnSetFlag -= OnSetFlag;
        hpController.OnDie -= OnDie;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.RegisterNPC(gameObject);
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

    private void Update()
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
        if ( targetFlag.isActiveAndEnabled && inAttarctRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetFlag.transform.position, moveToFlagSpeed * Time.deltaTime);
        }
    }

    private void OnDie()
    {
        gameManager.UnregisterNPC(gameObject);
    }
}
