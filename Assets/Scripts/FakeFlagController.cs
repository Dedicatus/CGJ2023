using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.Events;

public class FakeFlagController : MonoBehaviour
{
    public Requirement requirement;
    public float attractRadius;
    [SerializeField]
    private LayerMask npcLayer;
    private List<GameObject> attractedNpc;
    [SerializeField]
    private SpriteRenderer renderSprite;
    [SerializeField]
    private GameObject rangeIndicator;
    private SphereCollider overlapCollider;
    private Color arrowColor;

    private Collider[] colliders;

    private void OnEnable()
    {

    }
    void Start()
    {
        
    }

    void Update()
    {
       DetectOverlappingObjects();
    }

    public void InitFakeFlag(Requirement commingRequirement, Sprite sprite,float radius,Color highlighColor)
    {
        overlapCollider = GetComponent<SphereCollider>();
        attractRadius = radius;
        overlapCollider.radius = radius;
        rangeIndicator.transform.localScale = new Vector3(radius, 1, radius);
        renderSprite.sprite = sprite;
        requirement = commingRequirement;
        arrowColor = highlighColor;
    }

    void DetectOverlappingObjects()
    {
        int layerMask = LayerMask.GetMask("Npc"); // Get the layer mask for "npc" layer
        colliders = Physics.OverlapSphere(transform.position, attractRadius, layerMask); // Get all colliders overlapping with the sphere
        Debug.Log(colliders.Length);
        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent(out RequirementController requirementController))
            {
                if(requirementController.requirement == requirement)
                {
                    requirementController.arrow.GetComponentInChildren<SpriteRenderer>().color = arrowColor;
                    requirementController.hightHat.SetActive(true);
                    requirementController.arrow.SetActive(true);
                    requirementController.arrow.transform.forward =  gameObject.transform.position - collider.transform.position;
                }
            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggleEnd");
        if (other.gameObject.TryGetComponent(out RequirementController requirementController))
        {
            requirementController.hightHat.SetActive(false);
            requirementController.arrow.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out RequirementController requirementController))
            {
                requirementController.hightHat.SetActive(false);
                requirementController.arrow.SetActive(false);
            }
        }
    }
}
