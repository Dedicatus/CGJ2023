using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RequirementController : MonoBehaviour
{
    public List<Requirement> curRequirement = new();
    public List<Requirement> historyRequirement = new();
    public List<Requirement> requirementPool = new();

    private void Awake()
    {
        requirementPool = Enum.GetValues(typeof(Requirement)).Cast<Requirement>().ToList();
        requirementPool.RemoveAll(t => t == Requirement.none);
    }

    private Requirement GetRandomRequirement()
    {
        var index = UnityEngine.Random.Range(0, requirementPool.Count);
        return requirementPool[index];
    }

    [ContextMenu(nameof(GetFirstRequirement))]
    public Requirement GetFirstRequirement()
    {
        var requirement = GetRequirement(0);
#if DEBUG
        Debug.Log($"GetFirstRequirement: {requirement}");
#endif
        return requirement;
    }

    [ContextMenu(nameof(GetSecondRequirement))]
    public Requirement GetSecondRequirement()
    {
        var requirement = GetRequirement(1);
#if DEBUG
        Debug.Log($"GetFirstRequirement: {requirement}");
#endif
        return requirement;
    }

    public void SatisfyRequirement(Requirement requirement)
    {
        curRequirement.Remove(requirement);
        historyRequirement.Add(requirement);
    }


    private Requirement GetRequirement(int i)
    {
        return curRequirement.Count > i ? curRequirement[i] : GetRandomRequirement();
    }
}