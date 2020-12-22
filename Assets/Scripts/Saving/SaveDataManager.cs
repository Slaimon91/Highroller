using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    [SerializeField] GameObject saveIconGO;
    private void Awake()
    {
        GameEvents.SaveInitiatedData += Save;
        GameEvents.LoadInitiatedData += Load;
    }

    public void Save(string temp)
    {
        if(temp == "")
        {
            GameObject popup = Instantiate(saveIconGO, FindObjectOfType<OverworldCanvas>().transform);
            popup.GetComponent<FadeBox>().AssignInfo("", 0.8f);
            SaveSystem.Save<SaveData>(SaveData.current, playerValues.currentSavefile + "/" + "temp/" + playerValues.currentOWScene + "/SaveData");
            SaveSystem.OverrideWithTemp(playerValues.currentSavefile.ToString());
        }
        else
        {
            SaveSystem.Save<SaveData>(SaveData.current, playerValues.currentSavefile + "/" + temp + playerValues.currentOWScene + "/SaveData");
        }
    }

    public void Load(string temp)
    {
        SaveData data = SaveSystem.Load<SaveData>(playerValues.currentSavefile + "/" + temp + playerValues.currentOWScene + "/SaveData");

        if (data != default)
        {
            SaveData.current.playerOW = data.playerOW;
            SaveData.current.seedGiver = data.seedGiver;
            SaveData.current.moleGlasses = data.moleGlasses;
            SaveData.current.tricksterBridge = data.tricksterBridge;
            SaveData.current.inventory = data.inventory;
            SaveData.current.timeManager = data.timeManager;

            foreach (SavestoneData savestone in data.saveStones)
            {
                SaveData.current.saveStones.Add(savestone);
            }

            foreach (ResourcePickupData resourcePickup in data.resourcePickups)
            {
                SaveData.current.resourcePickups.Add(resourcePickup);
            }

            foreach (GaiablockData gblock in data.gaiablocks)
            {
                SaveData.current.gaiablocks.Add(gblock);
            }

            foreach (TileTreasureData tileTreasure in data.tileTreasures)
            {
                SaveData.current.tileTreasures.Add(tileTreasure);
            }

            foreach (TileTreasureData tileTreasure in data.tileTreasures)
            {
                SaveData.current.tileTreasures.Add(tileTreasure);
            }

            foreach (DialogueTriggerData dialogueTrigger in data.dialogueTriggers)
            {
                SaveData.current.dialogueTriggers.Add(dialogueTrigger);
            }

            foreach (VineWeaverGOData vineWeaver in data.vineWeavers)
            {
                SaveData.current.vineWeavers.Add(vineWeaver);
            }

            foreach (ItemPickupsData itemPickup in data.itemPickups)
            {
                SaveData.current.itemPickups.Add(itemPickup);
            }

            foreach (BerryTileData berry in data.berryTiles)
            {
                SaveData.current.berryTiles.Add(berry);
            }
        }

        else
        {
            Reset();
        }
    }

    public void Reset()
    {
        SaveData.current.playerOW = null;
        SaveData.current.seedGiver = null;
        SaveData.current.moleGlasses = null;
        SaveData.current.tricksterBridge = null;
        SaveData.current.inventory = null;
        SaveData.current.timeManager = null;

        SaveData.current.saveStones.Clear();

        SaveData.current.resourcePickups.Clear();

        SaveData.current.gaiablocks.Clear();

        SaveData.current.tileTreasures.Clear();

        SaveData.current.dialogueTriggers.Clear();

        SaveData.current.vineWeavers.Clear();

        SaveData.current.itemPickups.Clear();

        SaveData.current.berryTiles.Clear();
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiatedData -= Save;
        GameEvents.LoadInitiatedData -= Load;
    }
}

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public PlayerData playerOW;
    public SeedGiverData seedGiver;
    public MoleGlassesData moleGlasses;
    public TricksterBridgeData tricksterBridge;
    public InventoryData inventory;
    public TimeManagerData timeManager;

    public List<SavestoneData> saveStones = new List<SavestoneData>();

    public List<ResourcePickupData> resourcePickups = new List<ResourcePickupData>();

    public List<GaiablockData> gaiablocks = new List<GaiablockData>();

    public List<TileTreasureData> tileTreasures = new List<TileTreasureData>();

    public List<DialogueTriggerData> dialogueTriggers = new List<DialogueTriggerData>();

    public List<VineWeaverGOData> vineWeavers = new List<VineWeaverGOData>();

    public List<ItemPickupsData> itemPickups = new List<ItemPickupsData>();

    public List<BerryTileData> berryTiles = new List<BerryTileData>();
}