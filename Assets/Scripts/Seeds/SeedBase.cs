using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBase : MonoBehaviour
{
    [SerializeField] protected string seedName;
    [SerializeField] protected string berryName;

    [SerializeField]
    [TextArea(3, 20)]
    protected string info;

    protected GameObject seedImage;

    [SerializeField] Sprite seedSprite;
    [SerializeField] Sprite berrySprite;

    protected bool inactive = false;

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

    public GameObject GetSeedImage()
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

    public int GetInventorySlotNr()
    {
        return inventorySlotNr;
    }
}
