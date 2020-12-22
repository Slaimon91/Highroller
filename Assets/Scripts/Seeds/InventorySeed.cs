using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySeed : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    [SerializeField] GameObject selected;
    [SerializeField] GameObject selectedBerry;
    [SerializeField] GameObject inactive;
    private bool isInactive = false;
    private bool created = false;
    private bool isBerry = false;

    [SerializeField] Image infoImage;
    [SerializeField] Image infoClockImage;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI infoTimeText;
    [SerializeField] Sprite emptyBerry;
    [SerializeField] Sprite emptyClock;
    [SerializeField] Sprite clock;
    //[SerializeField] GameObject popupPrefab;

    private string seedName = "";
    private string berryName = "";
    private string seedText = "";
    private string timeText = "";
    private Sprite seedSprite;
    private Sprite berrySprite;
    private Sprite clockSprite;

    InventoryUI inventoryUI;

    void Awake()
    {
        seedSprite = emptyBerry;
        clockSprite = emptyClock;
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Created()
    {
        if (created)
        {
            return;
        }

        if (GetComponentInChildren<SeedBase>() != null)
        {
            SeedBase seed = GetComponentInChildren<SeedBase>();

            seedName = seed.GetSeedName();
            berryName = seed.GetBerryName();
            seedText = seed.GetInfo();
            seedSprite = seed.GetSeedSprite();
            berrySprite = seed.GetBerrySprite();
            timeText = seed.GetGrowthTimeString();
            clockSprite = clock;

            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        if(isBerry)
        {
            selectedBerry.SetActive(true);
            infoName.text = berryName;
            infoImage.sprite = berrySprite;
            infoImage.SetNativeSize();
        }
        else
        {
            selected.SetActive(true);
            infoName.text = seedName;
            infoImage.sprite = seedSprite;
            infoImage.SetNativeSize();
        }

        infoText.text = seedText;
        infoTimeText.text = timeText;
        infoClockImage.sprite = clockSprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        if (isBerry)
        {
            selectedBerry.SetActive(false);
        }
        else
        {
            selected.SetActive(false);
        }
    }

    public void ForceDeselect()
    {
        buttonSelected = false;

        selectedBerry.SetActive(false);
        selected.SetActive(false);
    }

    public void ConsumeBerry()
    {
        if(isBerry)
        {
            GetComponentInChildren<SeedBase>().ConsumeBerry();
            infoName.text = seedName;
            infoImage.sprite = seedSprite;
            infoImage.SetNativeSize();
        }
    }

    public void ToggleInactivateSlot(bool status)
    {
        if(status)
        {
            inactive.SetActive(true);
            isInactive = true;
        }
        else
        {
            inactive.SetActive(false);
            isInactive = false;
        }
        
    }

    public void SetBerryStatus(bool status)
    {
        isBerry = status;
    }

    public bool GetBerryStatus()
    {
        return isBerry;
    }

    public bool GetStatus()
    {
        return isInactive;
    }
}
