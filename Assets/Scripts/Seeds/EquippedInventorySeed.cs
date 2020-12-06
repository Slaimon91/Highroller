using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquippedInventorySeed : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] Image plantImageHolder;
    [SerializeField] Sprite[] plantStages;
    [SerializeField] Sprite emptyBerry;
    private int seedStage = 0;
    private Sprite berrySprite;
    private int growthMultiplier = 1;
    private bool hasEquippedSeed = false;

    [SerializeField] PlayerValues playerValues;

    void Start()
    {
        infoName.text = "None";
        plantImageHolder.sprite = emptyBerry;
    }

    public void Created(SeedBase seedToEquip)
    {
        berrySprite = seedToEquip.GetBerrySprite();
        infoName.text = seedToEquip.GetBerryName();

        growthMultiplier = seedToEquip.GetGrowthMultiplier();
        //playerValues.seedStage = 0;
        hasEquippedSeed = true;
        OnEnable();
    }

    void OnEnable()
    {
        if(!hasEquippedSeed)
        {
            return;
        }
        //seedStage = playerValues.seedStage;

        if(seedStage < 1*growthMultiplier)
        {
            plantImageHolder.sprite = plantStages[0];
        }
        else if (seedStage < 2*growthMultiplier)
        {
            plantImageHolder.sprite = plantStages[1];
        }
        else if (seedStage < 3*growthMultiplier)
        {
            plantImageHolder.sprite = plantStages[2];
        }
        else if (seedStage >= 3*growthMultiplier || growthMultiplier == 0)
        {
            plantImageHolder.sprite = berrySprite;
        }
    }
}
