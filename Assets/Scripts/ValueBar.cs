using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable Unity.InefficientPropertyAccess

[RequireComponent(typeof(Slider))]
public class ValueBar : SerializedMonoBehaviour
{
    public Color[] gradient;

    public float scale = 0.1f;
    public float offsetY = 4;
    public float offsetX;

    [ReadOnly] [SerializeField] private Slider slider;

    public Slider Slider
    {
        get
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }

            return slider;
        }
    }

    public Transform character;

    public void Update()
    {
        transform.position = character.position;
        if (gradient.Length >= 2)
        {
            Slider.fillRect.GetComponent<Image>().color = Color.Lerp(gradient[0], gradient[1], Slider.normalizedValue);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = character.rotation;
        transform.position = character.position + character.up * offsetY;
        transform.position += character.right * offsetX;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}