using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    private void Awake()
    {
        GameEvents.SaveInitiatedData += Save;
        GameEvents.LoadInitiatedData += Load;
    }

    public void Save(string temp)
    {
        if(temp == "")
        {
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

        SaveData.current.saveStones.Clear();

        SaveData.current.resourcePickups.Clear();

        SaveData.current.gaiablocks.Clear();
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

    public List<SavestoneData> saveStones = new List<SavestoneData>();

    public List<ResourcePickupData> resourcePickups = new List<ResourcePickupData>();

    public List<GaiablockData> gaiablocks = new List<GaiablockData>();
}