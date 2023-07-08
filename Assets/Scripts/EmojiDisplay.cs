using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable Unity.InefficientPropertyAccess

public class EmojiDisplay : SerializedMonoBehaviour
{
    [SerializeField] private Image image;

    public float scale = 0.1f;
    public float offsetY = 4;
    
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

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}