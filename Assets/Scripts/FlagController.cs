using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagController : MonoBehaviour
{
    public static UnityAction<FlagController> OnSetFlag;
    public Requirement requirement;
    public float formationRadius;
    public float attractRadius;
    public float keepTime=9999;
    private float currentTime=9999;

    private void OnEnable()
    {
        OnSetFlag?.Invoke(this);
    }

    void Start()
    {
        currentTime = keepTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public bool InFormationRange(Vector3 position)
    {
        return (position - transform.position).magnitude < formationRadius;
    }
    public bool InAttarctRange(Vector3 position)
    {
        return (position - transform.position).magnitude < attractRadius;
    }

    public void InitFlag(Requirement commingRequirement)
    {
        requirement = commingRequirement;
    }
}
