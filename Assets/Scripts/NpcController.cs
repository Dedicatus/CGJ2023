using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float moveToFlagSpeed;
    [ShowInInspector]
    private FlagController targetFlag;
    [ShowInInspector]
    private Vector3 targetPos;
    private GameManager gameManager;
    private HpController hpController;
    private Animator animator;
    private AutoEvade autoEvade;

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
        animator = GetComponent<Animator>();
        GetComponent<Rigidbody>().sleepThreshold = 0f;
        autoEvade = GetComponentInChildren<AutoEvade>();
        autoEvade.WalkLeft += () =>
        {
            animator.SetTrigger("OnWalk");
            GetComponentInChildren<FaceToCamera>().FaceLeft();
        };
        autoEvade.WalkRight += () =>
        {
            animator.SetTrigger("OnWalk");
            GetComponentInChildren<FaceToCamera>().FaceRight();
        };
        autoEvade.EndWalk += () =>
        {
            animator.SetTrigger("OnIdle");
        };
    }

    private void OnSetFlag(FlagController flagCtrl)
    {
        if (targetFlag != null && targetFlag.isActiveAndEnabled)
        {
            return;
        }
        if (flagCtrl.requirement == GetComponent<RequirementController>().requirement && flagCtrl.InAttarctRange(transform.position))
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
        MoveToFormationPosition();
    }
    private void MoveToFormationPosition()
    {
        if (targetPos == Vector3.zero)
        {
            return;
        }
        if (targetFlag == null || !targetFlag.isActiveAndEnabled)
        {
            if (!autoEvade.isWalking && targetPos != Vector3.zero)
            {
                targetPos = Vector3.zero;
                animator.SetTrigger("OnIdle");
            }
            return;
        }
        if ((targetPos - transform.position).magnitude > 0.12f)
        {
            animator.SetTrigger("OnWalk");

            if (targetPos.x > transform.position.x)
            {
                GetComponentInChildren<FaceToCamera>().FaceRight();
            }
            else
            {
                GetComponentInChildren<FaceToCamera>().FaceLeft();
            }
        }
        else if ((targetPos - transform.position).magnitude <= 0.12f)
        {
            targetPos = Vector3.zero;
            animator.SetTrigger("OnIdle");
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveToFlagSpeed * Time.deltaTime);
    }
    public void SetTargetPosition(Vector3 pos)
    {
        targetPos = Vector3.Scale(pos, new Vector3(1f, 0f, 1f));
    }

    private void OnDie()
    {
        gameManager.UnregisterNPC(gameObject);
    }
}
