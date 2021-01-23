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
    [HideInInspector] public bool berryIsPlanted = false;

    [HideInInspector] public int berryStage = 0; //0, 1, 2, 3, 4
    public float berryPlantedAtTime = 0;
    [HideInInspector] public int berryGrowthSpeed = 0; //1, 2, 3
    public float berryGrowthPoints = 0; //3, 6, 9        4, 8, 12         5, 10, 15,        5,10,15 minutes

    private GenericTextManager genericTextManager;
    [SerializeField] GameObject rewardbox;
    [HideInInspector] public string id;
    [HideInInspector] public string plantedBerryName;
    private TimeManager timeManager;
    private InventoryUI inventoryUI;

    void Awake()
    {
        plantSprite = GetComponent<SpriteRenderer>();
        timeManager = FindObjectOfType<TimeManager>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }
    void Update()
    {
        if(berryStage != 5)
        {
            ReloadBerries();
        }

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
        berryPlantedAtTime = 0;
        plantSprite.sprite = plantStageZero;
        plantedBerryName = "";
        plantedBerry.SetBerryStatus(true);
        plantedBerry.SetInactiveStatus(false);

        string itemIntro = "You picked up a";
        string itemText = plantedBerry.GetBerryName();
        GameObject popup = Instantiate(rewardbox, FindObjectOfType<RewardboxParent>().transform);
        popup.GetComponent<Rewardbox>().AssignInfo(itemIntro, itemText, plantedBerry.GetBerrySprite());
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(63f / 255f, 202f / 255f, 184f / 255f));

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
        berryPlantedAtTime = timeManager.GetTimeSeconds();
        berryIsPlanted = true;
        berryGrowthPoints = 0;
        berryStage = 1;
        plantSprite.sprite = plantStageOne;
        plantedBerry = berry;
        plantedBerryName = berry.GetSeedName();
        berryGrowthSpeed = berry.GetGrowthMultiplier();
        plantStageFive = berry.GetBerryTileFinishedSprite();
        berry.SetInactiveStatus(true);
    }

    private void AdvanceBerryStage()
    {
        if (berryGrowthPoints < 60) //Stage 0
        {
            berryStage = 1;
            plantSprite.sprite = plantStageOne;
        }
        if (berryGrowthPoints > 60 && berryGrowthPoints < (300 * berryGrowthSpeed) * 0.33) //Stage 2 = 3, 4, 5
        {
            plantSprite.sprite = plantStageTwo;
            berryStage = 2;
        }
        if (berryGrowthPoints > (300 * berryGrowthSpeed) * 0.333 && berryGrowthPoints < ((300 * berryGrowthSpeed) * 0.666)) //Stage 3 = 2, 3, 4
        {
            plantSprite.sprite = plantStageThree;
            berryStage = 3;
        }

        else if (berryGrowthPoints > (300 * berryGrowthSpeed) * 0.666 && berryGrowthPoints < (300 * berryGrowthSpeed)) //Stage 4 = 4, 6, 8
        {
            plantSprite.sprite = plantStageFour;
            berryStage = 4;
        }

        else if (berryGrowthPoints > (300 * berryGrowthSpeed)) //Stage 5 = 6, 9, 12
        {
            plantSprite.sprite = plantStageFive;
            berryStage = 5;
        }
    }

    /*private void OnEnable()
    {
        if(berryIsPlanted)
        {
            berryGrowthPoints = playerValues.nrOfBattles - berryPlantedAtBattleNR;
            AdvanceBerryStage();
        }
    }*/

    public void ReloadBerries()
    {
        if (berryIsPlanted)
        {
            berryGrowthPoints = timeManager.GetTimeSeconds() - berryPlantedAtTime;
            AdvanceBerryStage();
        }
    }
    private void Save(string temp)
    {
        SaveData.current.berryTiles.Add(new BerryTileData(gameObject.GetComponent<BerryTile>()));
    }

    public void Load(string temp)
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI.inventoryFinishedLoading)
        {
            LoadTile();
        }
        else
        {
            FindObjectOfType<InventoryUI>().onInventoryFinishedLoadingCallback += LoadTile;
        }
    }

    public void LoadTile()
    {
        BerryTileData data = SaveData.current.berryTiles.Find(x => x.id == id);
        if (data != default && data.berryIsPlanted)
        {
            id = data.id;
            berryIsPlanted = data.berryIsPlanted;

            berryStage = data.berryStage;
            berryPlantedAtTime = data.berryPlantedAtTime;
            berryGrowthSpeed = data.berryGrowthSpeed;
            berryGrowthPoints = data.berryGrowthPoints;

            Item itemSeed = Resources.Load("ScriptableObjects/Seeds/" + data.plantedBerryName) as Item;
            plantedBerry = FindObjectOfType<InventoryUI>().GetSeed(itemSeed.prefab.GetComponent<SeedBase>());

            plantStageFive = plantedBerry.GetBerryTileFinishedSprite();
            plantedBerryName = plantedBerry.GetSeedName();
            ReloadBerries();
        }
        FindObjectOfType<InventoryUI>().onInventoryFinishedLoadingCallback -= LoadTile;
    }
    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}

[System.Serializable]
public class BerryTileData
{
    public string id;
    public bool berryIsPlanted;
    public string plantedBerryName;

    public int berryStage;
    public float berryPlantedAtTime;
    public int berryGrowthSpeed;
    public float berryGrowthPoints;

    public BerryTileData(BerryTile berryTile)
    {
        id = berryTile.id;
        berryIsPlanted = berryTile.berryIsPlanted;
        plantedBerryName = berryTile.plantedBerryName;
        berryStage = berryTile.berryStage;
        berryPlantedAtTime = berryTile.berryPlantedAtTime;
        berryGrowthSpeed = berryTile.berryGrowthSpeed;
        berryGrowthPoints = berryTile.berryGrowthPoints;
    }
}