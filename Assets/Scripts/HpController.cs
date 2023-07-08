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


    public ValueBar valueBar;
    public float maxLimit = 100;
    public float hpLimit = 100;
    public float hpCur = 50;

    public UnityAction OnDie;
    public static UnityAction<Transform> OnNpcDie;

    public UnityEvent dieEvent;

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
        if (!(hpCur <= 0))
        {
            return;
        }

        OnDie?.Invoke();
        dieEvent?.Invoke();
        OnNpcDie?.Invoke(transform);
    }
}