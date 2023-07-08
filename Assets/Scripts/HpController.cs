using UnityEngine;
using UnityEngine.Events;

public class HpController : MonoBehaviour
{
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