using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryTile : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerValues playerValues;
    private SpriteRenderer plantSprite;
    [SerializeField] Sprite plantStageZero;
    [SerializeField] Sprite plantStageOne;
    [SerializeField] Sprite plantStageTwo;
    [SerializeField] Sprite plantStageThree;
    [SerializeField] Sprite plantStageFour;
    private Sprite plantStageFive;
    private SeedBase plantedBerry;
    private bool berryIsPlanted = false;
    private int berryStage = 0; //0, 1, 2, 3, 4
    private int berryPlantedAtBattleNR = 0;
    private int berryGrowthSpeed = 0; //1, 2, 3
    private int berryGrowthPoints = 0; //3, 6, 9        4, 8, 12         5, 10, 15
    private GenericTextManager genericTextManager;

    void Awake()
    {
        plantSprite = GetComponent<SpriteRenderer>();
    }
    public void Interact()
    {
        if(berryIsPlanted)
        {
            if(berryStage == 5)
            {
                HarvestBerry();
            }
            else
            {
                BerryDialogue();
            }
        }
        else
        {
            BerryChoice berryChoice;
            if ((berryChoice = FindObjectOfType<BerryChoice>()) != null)
            {
                if(!berryChoice.TriggerBerryChoice(gameObject.GetComponent<BerryTile>()))
                {
                    BerryDialogue();
                }
            }
        }
    }

    private void HarvestBerry()
    {
        berryIsPlanted = false;
        berryPlantedAtBattleNR = 0;
        plantSprite.sprite = plantStageZero;
        plantedBerry.SetBerryStatus(true);
        plantedBerry.SetInactiveStatus(false);
        plantedBerry = null;
    }

    private void BerryDialogue()
    {
        if(genericTextManager == null)
        {
            genericTextManager = FindObjectOfType<GenericTextManager>();
        }
        
        if (berryStage == 0)
        {
            genericTextManager.DisplayText("A seed could be planted here.");
        }

        if (berryStage == 1)
        {
            genericTextManager.DisplayText("A <color=#BCB845>" + plantedBerry.GetSeedName() + "</color>" + " was planted recently.");
        }

        else if (berryStage == 2)
        {
            genericTextManager.DisplayText("The <color=#BCB845>" + plantedBerry.GetSeedName() + "</color>" + " is just starting to grow.");
        }

        else if (berryStage == 3)
        {
            genericTextManager.DisplayText("The <color=#BCB845>" + plantedBerry.GetSeedName() + "</color>" + " is growing rapidly.");
        }

        else if (berryStage == 4)
        {
            genericTextManager.DisplayText("The <color=#BCB845>" + plantedBerry.GetBerryName() + "</color>" + " looks almost ready for harvest.");
        }
    }

    public void PlantBerry(SeedBase berry) //Stage 1
    {
        berryPlantedAtBattleNR = playerValues.nrOfBattles;
        berryIsPlanted = true;
        berryGrowthPoints = 0;
        berryStage = 1;
        plantSprite.sprite = plantStageOne;
        plantedBerry = berry;
        berryGrowthSpeed = berry.GetGrowthMultiplier();
        plantStageFive = berry.GetBerryTileFinishedSprite();
        berry.SetInactiveStatus(true);
    }

    private void AdvanceBerryStage()
    {
        if (berryGrowthPoints == 1) //Stage 2 = 3, 4, 5
        {
            plantSprite.sprite = plantStageTwo;
            berryStage = 2;
        }
        if (berryGrowthPoints > 2 + berryGrowthSpeed && berryGrowthPoints < (2 + berryGrowthSpeed) * 2) //Stage 3 = 3, 4, 5
        {
            plantSprite.sprite = plantStageThree;
            berryStage = 3;
        }

        else if (berryGrowthPoints > (2 + berryGrowthSpeed) * 2 && berryGrowthPoints < (2 + berryGrowthSpeed) * 3) //Stage 4 = 6, 8, 10
        {
            plantSprite.sprite = plantStageFour;
            berryStage = 4;
        }

        else if (berryGrowthPoints > (2 + berryGrowthSpeed) * 3 && berryStage <= 4) //Stage 5 = 9, 12, 15
        {
            plantSprite.sprite = plantStageFive;
            berryStage = 5;
        }
    }

    private void OnEnable()
    {
        if(berryIsPlanted)
        {
            berryGrowthPoints = playerValues.nrOfBattles - berryPlantedAtBattleNR;
            AdvanceBerryStage();
        }
    }

    public void ReloadBerries()
    {
        if (berryIsPlanted)
        {
            berryGrowthPoints = playerValues.nrOfBattles - berryPlantedAtBattleNR;
            AdvanceBerryStage();
        }
    }
}
