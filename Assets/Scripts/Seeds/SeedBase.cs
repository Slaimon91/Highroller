using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SeedBase : MonoBehaviour
{
    [SerializeField] protected string seedName;
    [SerializeField] protected string berryName;

    [SerializeField]
    [TextArea(3, 20)]
    protected string info;

    [SerializeField] Image seedImage;

    [SerializeField] Sprite seedSprite;
    [SerializeField] Sprite berrySprite;
    [SerializeField] Sprite berryTileFinishedSprite;

    protected bool inactive = false;
    protected bool isBerry = false;

    [SerializeField] protected bool shortGrowth = false;
    [SerializeField] protected bool mediumGrowth = false;
    [SerializeField] protected bool longGrowth = false;

    [SerializeField] protected int inventorySlotNr;

    void Awake()
    {
        transform.SetSiblingIndex(0);
    }

    //Getters & Setters
    public string GetSeedName()
    {
        return seedName;
    }

    public string GetBerryName()
    {
        return berryName;
    }

    public string GetInfo()
    {
        return info;
    }

    public Image GetSeedImage()
    {
        return seedImage;
    }

    public Sprite GetBerrySprite()
    {
        return berrySprite;
    }

    public Sprite GetSeedSprite()
    {
        return seedSprite;
    }

    public Sprite GetBerryTileFinishedSprite()
    {
        return berryTileFinishedSprite;
    }

    public string GetGrowthTimeString()
    {
        if(shortGrowth)
        {
            return "Short";
        }
        else if (mediumGrowth)
        {
            return "Medium";
        }
        else if (longGrowth)
        {
            return "Long";
        }
        else
        {
            return "Instant";
        }
    }

    public int GetGrowthMultiplier()
    {
        if (shortGrowth)
        {
            return 1;
        }
        else if (mediumGrowth)
        {
            return 2;
        }
        else if (longGrowth)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public bool GetInactiveStatus()
    {
        return inactive;
    }

    public bool GetBerryStatus()
    {
        return isBerry;
    }

    public int GetInventorySlotNr()
    {
        return inventorySlotNr;
    }

    public void SetBerryStatus(bool status)
    {
        isBerry = status;
        GetComponentsInParent<InventorySeed>(true)[0].SetBerryStatus(status);

        if (isBerry)
        {
            seedImage.sprite = berrySprite;
            seedImage.SetNativeSize();
        }
        else
        {
            seedImage.sprite = seedSprite;
            seedImage.SetNativeSize();
        }
    }

    public void SetInactiveStatus(bool status)
    {
        inactive = status;

        GetComponentsInParent<InventorySeed>(true)[0].ToggleInactivateSlot(status);
    }

    public virtual void ConsumeBerry()
    {
        SetBerryStatus(false);
        //GetComponentsInParent<InventorySeed>(true)[0].ForceDeselect();
        PlayerControlsManager playerControlsManager;
        if ((playerControlsManager = FindObjectOfType<PlayerControlsManager>()) != null)
        {
            playerControlsManager.TriggerCloseInventory();
        }
    }
}
