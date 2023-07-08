using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable Unity.InefficientPropertyAccess

public class EmojiDisplay : SerializedMonoBehaviour
{
    [SerializeField] private Image image;

    public float scale = 0.1f;
    public float offsetY = 4;
    public float offsetX = 4;
    public Transform character;

    private void Update()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        transform.rotation = character.rotation;
        transform.position = character.position + character.up * offsetY;
        transform.position += character.right * offsetX;
        transform.localScale = new Vector3(scale, scale, scale);
    }


    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}