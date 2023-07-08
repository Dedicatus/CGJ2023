using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagController : MonoBehaviour
{
    public static UnityAction<FlagController> OnSetFlag;
    public Requirement requirement;
    public float formRadius;
    public float attractRadius;

    private void OnEnable()
    {
        OnSetFlag?.Invoke(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public bool InFormRange(Vector3 position)
    {
        return (position - transform.position).magnitude < formRadius;
    }
    public bool InAttarctRange(Vector3 position)
    {
        return (position - transform.position).magnitude < attractRadius;
    }
}
