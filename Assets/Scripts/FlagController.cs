using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.Events;

public class FlagController : MonoBehaviour
{
    public static UnityAction<FlagController> OnSetFlag;
    public Requirement requirement;
    public float formationRadius;
    public float attractRadius;
    public float keepTime = 9999;
    [SerializeField]
    private GameObject distribution;
    [SerializeField]
    private LayerMask distributionLayer;
    [SerializeField]
    private LayerMask npcLayer;
    private float currentTime = 9999;
    private List<GameObject> attractedNpc;
    private List<Transform> attractedNpcTarget;
    [SerializeField]
    private SpriteRenderer flagRender;
    [SerializeField]
    private GameObject rangeIndicator;
    [SerializeField]
    private Renderer slider;

    private void OnEnable()
    {
        attractedNpc = new();
        attractedNpcTarget = new();
    }

    private IEnumerator<float> GetFormationPosition()
    {
        distribution.transform.localScale = Vector3.one * formationRadius / 1.5f;
        distribution.transform.position = transform.position;
        while (attractedNpcTarget.Count < attractedNpc.Count)
        {
            attractedNpcTarget.Clear();
            foreach (var col in Physics.OverlapSphere(transform.position, formationRadius, distributionLayer))
            {
                if (col.transform.parent.parent != transform)
                {
                    continue;
                }
                attractedNpcTarget.Add(col.transform);
            }
            print(attractedNpcTarget.Count + ": " + attractedNpc.Count);
            distribution.transform.localScale *= 0.9f;
            yield return Timing.WaitForSeconds(0.2f);
        }
        var i = 0;
        foreach (var npc in attractedNpc)
        {
            npc.GetComponent<NpcController>().SetTargetPosition(attractedNpcTarget[i].position);
            i++;
        }
    }
    void Start()
    {
        currentTime = keepTime;
        OnSetFlag?.Invoke(this);
        foreach (var col in Physics.OverlapSphere(transform.position, attractRadius, npcLayer))
        {
            if (col.TryGetComponent(out RequirementController rqrCtrl) && rqrCtrl.requirement == requirement)
            {
                attractedNpc.Add(col.gameObject);
            }
        }
        Timing.RunCoroutine(GetFormationPosition());
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        slider.material.SetFloat("_SliderAmount", currentTime / keepTime);
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

    public void InitFlag(Requirement commingRequirement, Sprite sprite,float radius,Color sliderColor)
    {
        requirement = commingRequirement;
        flagRender.sprite = sprite;
        attractRadius = radius;
        Debug.Log(sliderColor);
        slider.GetComponent<SpriteRenderer>().color = sliderColor;
        rangeIndicator.transform.localScale = new Vector3(attractRadius,1, attractRadius);
    }
}
