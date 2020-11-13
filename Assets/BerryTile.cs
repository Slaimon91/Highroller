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

    void Awake()
    {
        plantSprite = GetComponent<SpriteRenderer>();
    }
    public void Interact()
    {
        if(berryIsPlanted)
        {
            if(berryStage == 4)
            {
                HarvestBerry();
            }
            BerryDialogue();
        }
        else
        {
            BerryChoice berryChoice;
            if ((berryChoice = FindObjectOfType<BerryChoice>()) != null)
            {
                berryChoice.TriggerBerryChoice(gameObject.GetComponent<BerryTile>());
            }
        }
    }

    private void HarvestBerry()
    {
        berryIsPlanted = false;
        berryPlantedAtBattleNR = 0;
    }

    private void BerryDialogue()
    {
        if(berryStage == 1)
        {

        }

        else if (berryStage == 2)
        {

        }

        else if (berryStage == 3)
        {

        }
    }

    public void PlantBerry(SeedBase berry) //Stage 1
    {
        berryPlantedAtBattleNR = playerValues.nrOfBattles;
        berryIsPlanted = true;
        berryGrowthPoints = 0;
        berryStage = 1;
        berryGrowthSpeed = berry.GetGrowthMultiplier();
        plantSprite.sprite = plantStageOne;
        plantStageFive = berry.GetBerrySprite();
    }

    private void AdvanceBerryStage()
    {
        if (berryGrowthPoints == 1) //Stage 2 = 3, 4, 5
        {
            plantSprite.sprite = plantStageTwo;
        }
        if (berryGrowthPoints > 2 + berryGrowthSpeed && berryGrowthPoints < (2 + berryGrowthSpeed) * 2) //Stage 3 = 3, 4, 5
        {
            plantSprite.sprite = plantStageThree;
        }

        else if (berryGrowthPoints > (2 + berryGrowthSpeed) * 2 && berryGrowthPoints < (2 + berryGrowthSpeed) * 3) //Stage 4 = 6, 8, 10
        {
            plantSprite.sprite = plantStageFour;
        }

        else if (berryGrowthPoints > (2 + berryGrowthSpeed) * 3) //Stage 5 = 9, 12, 15
        {
            plantSprite.sprite = null;//plantStageFour;
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
