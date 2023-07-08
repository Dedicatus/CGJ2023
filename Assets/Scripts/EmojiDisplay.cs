using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable Unity.InefficientPropertyAccess

[RequireComponent(typeof(Image))]
public class EmojiDisplay : SerializedMonoBehaviour
{
    [ReadOnly] [SerializeField] private Image image;

    public float scale = 0.1f;
    public float offsetY = 4;

    public Image Image
    {
        get
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            return image;
        }
    }

    public Transform character;

    public void Update()
    {
        transform.position = character.position;
    }

    private void LateUpdate()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        transform.position = character.position + Vector3.up * offsetY;
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}