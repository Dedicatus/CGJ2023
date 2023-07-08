using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[ExecuteAlways]
public class RequirementController : MonoBehaviour
{
    public Requirement requirement;
    public List<MeshRenderer> meshRenderers;
    public List<Requirement> curRequirement = new();
    public List<Requirement> historyRequirement = new();
    public List<Requirement> requirementPool = new();


    private void Awake()
    {
        requirementPool = Enum.GetValues(typeof(Requirement)).Cast<Requirement>().ToList();
        requirementPool.RemoveAll(t => t == Requirement.none);
        requirement = GetFirstRequirement();
    }

    private Requirement GetRandomRequirement()
    {
        var index = UnityEngine.Random.Range(0, requirementPool.Count);
        return requirementPool[index];
    }

    [ContextMenu(nameof(GetFirstRequirement))]
    public Requirement GetFirstRequirement()
    {
        var req = GetRequirement(0);
// #if DEBUG
//         Debug.Log($"GetFirstRequirement: {req}");
// #endif
        return req;
    }

    [ContextMenu(nameof(GetSecondRequirement))]
    public Requirement GetSecondRequirement()
    {
        var req = GetRequirement(1);
// #if DEBUG
//         Debug.Log($"GetFirstRequirement: {req}");
// #endif
        return req;
    }

    public void SatisfyRequirement(Requirement req)
    {
        curRequirement.Remove(req);
        historyRequirement.Add(req);
    }


    private Requirement GetRequirement(int i)
    {
        return curRequirement.Count > i ? curRequirement[i] : GetRandomRequirement();
    }


#if UNITY_EDITOR
    private RequirementColor _requirementColor;

    private RequirementColor RequirementColor
    {
        get
        {
            if (_requirementColor == null)
            {
                _requirementColor = Resources.Load<RequirementColor>("RequirementColor");
            }

            return _requirementColor;
        }
    }

    private void Update()
    {
        var graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            graphic.color = RequirementColor.GetColor(requirement);
        }

        foreach (var meshRenderer in meshRenderers)
        {
            var material = RequirementColor.GetMaterial(requirement);
            var color = material.color;
            color.a = 1;
            material.color = color;
            meshRenderer.material = material;
        }
    }
#endif
}