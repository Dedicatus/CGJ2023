#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SetRequirementColor : MonoBehaviour
{
    public Requirement requirement;

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
    }
}
#endif