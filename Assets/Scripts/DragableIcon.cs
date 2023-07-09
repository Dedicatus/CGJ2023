using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DragableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject fakeflagPrefab;
    public GameObject flagPrefab;
    [ShowInInspector,ReadOnly]
    private Requirement requirement;
    [ShowInInspector,ReadOnly]
    private float currentcd = 0;
    public float maxCD = 10;
    public float radius = 50;
    private bool canSpawn = true;
    private GameObject myFakeFlag;
    public static UnityAction<GameObject> flagSpawned;
    public Image flagImage;
    public Image coverImage;
    [SerializeField]
    private FlagDatas flagDatas;

    private AudioManager audioManager;

    private void Start() 
    {
        audioManager = AudioManager.Instance;
    }

    public void initFlagIcon(Requirement commmingRequirement, bool needCD = false)
    {
        requirement = commmingRequirement;
        flagImage.sprite = flagDatas.GetIconSprite(requirement);
        if (needCD)
        {
            currentcd = maxCD;
        }
        canSpawn = !needCD;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            // Store the initial position of the object and the mouse
            myFakeFlag = Instantiate(fakeflagPrefab);
            myFakeFlag.GetComponent<FakeFlagController>().InitFakeFlag(requirement, flagDatas.GetFlagSprite(requirement), radius, flagDatas.GetColor(requirement));
            audioManager.PlaySound("FlagDrag");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            LayerMask groundLayer = 1 << LayerMask.NameToLayer("Ground");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                // The raycast will only hit objects in the Ground layer
                myFakeFlag.transform.position = hit.point;
            }
            else
            {
                myFakeFlag.transform.position = Input.mousePosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canSpawn)
        {
            LayerMask groundLayer = 1 << LayerMask.NameToLayer("Ground");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                Destroy(myFakeFlag.gameObject);
                GameObject newFlag = Instantiate(flagPrefab, hit.point, Quaternion.identity);
                newFlag.GetComponent<FlagController>().InitFlag(requirement, flagDatas.GetFlagSprite(requirement),radius, flagDatas.GetColor(requirement));
                currentcd = maxCD;
                canSpawn = false;
                audioManager.PlaySound("UIClick");
                //flagSpawned.Invoke(gameObject);
            }
            else
            {
                Debug.Log("InvalidLoc");
                Destroy(myFakeFlag.gameObject); 
            }
        }
    }

    private void Update()
    {
        if (currentcd > 0)
        {
            currentcd -= Time.deltaTime;
            coverImage.fillAmount = currentcd / maxCD;
            if (currentcd <= 0)
            {
                canSpawn = true;
            }
        }
    }
}
