using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HpController : MonoBehaviour
{
    private Canvas _inGameCanvas;

    public Canvas InGameCanvas
    {
        get
        {
            if (_inGameCanvas == null)
            {
                _inGameCanvas = GameObject.Find("UIManager/InGameUI_WorldSpace/InGameCanvas").GetComponent<Canvas>();
                _inGameCanvas = _inGameCanvas ? _inGameCanvas : FindObjectOfType<Canvas>();
            }

            return _inGameCanvas;
        }
    }

    public ValueBar valueBarPrefab;

    public DeadBody deadBodyPrefab;
    public ValueBar valueBar;
    public float maxLimit = 100;
    public float hpLimit = 100;
    public float hpCur = 50;

    public UnityAction OnDie;
    public static UnityAction<Transform> OnNpcDie;

    public UnityEvent dieEvent;

    // public Transform[] walls;

    public float testValue;

    [ContextMenu(nameof(AddHp))]
    public void AddHp()
    {
        AddHp(testValue);
    }


    [ContextMenu(nameof(AddHpLimit))]
    public void AddHpLimit()
    {
        AddHp(0, testValue);
    }

    [ContextMenu(nameof(DecHp))]
    public void DecHp()
    {
        DecHp(testValue);
    }

    [ContextMenu(nameof(DecHpLimit))]
    public void DecHpLimit()
    {
        DecHp(0, testValue);
    }


    private void Awake()
    {
        if (valueBar == null)
        {
            valueBar = Instantiate(valueBarPrefab, InGameCanvas.transform);
            valueBar.character = transform;
        }
    }

    private void Update()
    {
        valueBar.Slider.value = hpCur / hpLimit;
    }

    public void AddHp(float hp, float limit = 0)
    {
        hpCur += hp;
        hpLimit += limit;
        hpLimit = Mathf.Clamp(hpLimit, 0, maxLimit);
        hpCur = Mathf.Clamp(hpCur, 0, hpLimit);
        Check();
    }

    public void DecHp(float hp, float limit = 0)
    {
        hpCur -= hp;
        hpLimit -= limit;
        hpLimit = Mathf.Clamp(hpLimit, 0, maxLimit);
        hpCur = Mathf.Clamp(hpCur, 0, hpLimit);
        Check();
    }

    private void Check()
    {
        if (hpCur <= 0 || hpCur >= hpLimit)
        {
            OnDie?.Invoke();
            dieEvent?.Invoke();
            OnNpcDie?.Invoke(transform);

            // 新建一个 然后退出
            var temp = Instantiate(deadBodyPrefab, transform.position, transform.rotation);

            temp.name = name + nameof(temp);
            // var all = temp.GetComponentsInChildren<MonoBehaviour>();
            // foreach (var component in all)
            // {
            //     var type = component.GetType();
            //     if (type == typeof(Transform) ||
            //         type == typeof(Rigidbody) ||
            //         type == typeof(MeshRenderer) ||
            //         type == typeof(MeshFilter))
            //     {
            //         continue;
            //     }
            //
            //     component.enabled = false;
            // }
            //
            // temp.AddComponent<DeadBody>();

            // FindNearestWall(temp.transform);

            gameObject.SetActive(false);
            valueBar.gameObject.SetActive(false);
            GetComponent<EmojiController>().emojiDisplay.gameObject.SetActive(false);
            // Destroy(valueBar);
            // Destroy(gameObject);
        }
    }

    // private void FindNearestWall(Transform target)
    // {
    //     Transform nearestWall = null;
    //     float minDistance = Mathf.Infinity;
    //     Vector3 currentPosition = target.position;
    //
    //     // 遍历所有墙体，找到最近的墙体
    //     foreach (Transform wall in walls)
    //     {
    //         float distance = Vector3.Distance(currentPosition, wall.position);
    //         if (distance < minDistance)
    //         {
    //             minDistance = distance;
    //             nearestWall = wall;
    //         }
    //     }
    //
    //     // 如果找到了最近的墙体，开始移动并淡出
    //     if (nearestWall != null)
    //     {
    //         StartCoroutine(MoveAndFade(target, nearestWall));
    //     }
    // }
    //
    // private IEnumerator MoveAndFade(Transform target, Transform targetWall)
    // {
    //     float duration = 2f; // 移动和淡出的持续时间
    //     float elapsedTime = 0f;
    //     Vector3 startPosition = target.position;
    //     Color startColor = GetComponent<Renderer>().material.color;
    //
    //     while (elapsedTime < duration)
    //     {
    //         // 移动物体向目标墙体
    //         target.position = Vector3.Lerp(startPosition, targetWall.position, elapsedTime / duration);
    //
    //         // 淡出物体颜色
    //         Color newColor = Color.Lerp(startColor, Color.clear, elapsedTime / duration);
    //         GetComponent<Renderer>().material.color = newColor;
    //
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     // 移动和淡出完成后，销毁物体
    //     // Destroy(gameObject);
    // }
}