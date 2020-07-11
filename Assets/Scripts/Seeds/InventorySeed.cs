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
    [SerializeField] GameObject inactive;
    private bool isInactive = false;
    private bool created = false;

    [SerializeField] Image infoImage;
    [SerializeField] Image infoClockImage;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI infoTimeText;
    [SerializeField] Sprite emptyBerry;
    [SerializeField] Sprite emptyClock;
    [SerializeField] Sprite clock;
    [SerializeField] GameObject popupPrefab;

    private string seedName = "";
    private string seedText = "";
    private string timeText = "";
    private Sprite seedSprite;
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
            seedText = seed.GetInfo();
            seedSprite = seed.GetSeedSprite();
            timeText = seed.GetGrowthTimeString();
            clockSprite = clock;

            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);

        infoName.text = seedName;
        infoText.text = seedText;
        infoImage.sprite = seedSprite;
        infoTimeText.text = timeText;
        infoClockImage.sprite = clockSprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
    }

    public void ForceDeselect()
    {
        buttonSelected = false;
        selected.SetActive(false);
    }

    public void Equip()
    {
        if(created && !isInactive)
        {
            GameObject popup = Instantiate(popupPrefab, gameObject.transform.parent.transform.parent.transform.parent);
            popup.GetComponent<PopupQuestion>().onYesAnswerCallback += YesEquip;
        }
    }

    public void YesEquip()
    {
        inventoryUI.EquipSeed(GetComponentInChildren<SeedBase>());
        isInactive = true;
        inactive.SetActive(true);
    }

    public void Unequip()
    {
        isInactive = false;
        inactive.SetActive(false);
    }
}
