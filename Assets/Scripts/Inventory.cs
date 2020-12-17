using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            // Destroy(gameObject);
            return;
        }
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;

        DontDestroyOnLoad(gameObject);

        /* if (instance != null)
         {
             Debug.LogWarning("More than one instance of inventory");
             return;
         }

         instance = this;*/
    }

    #endregion

    public delegate void OnItemChanged(Item item);
    public OnItemChanged onItemChangedCallback;

    public delegate void OnInventoryLoaded(List<Item> trinkets, List<Item> abilities, List<Item> seeds, bool clearInventory);
    public OnInventoryLoaded onInventoryLoadedCallback;

    public List<Item> trinkets = new List<Item>();
    public List<Item> abilities = new List<Item>();
    public List<Item> seeds = new List<Item>();

    public bool Add (Item item)
    {
        Debug.Log(item.prefab);
        //if it is an ability
        if(item.prefab.GetComponent<AbilityBase>() != null)
        {
            abilities.Add(item);
        }
        //if it is a seed
        else if (item.prefab.GetComponent<SeedBase>() != null)
        {
            seeds.Add(item);
        }
        //if it is an item
        else if (item.prefab.GetComponent<TrinketBase>() != null)
        {
            trinkets.Add(item);
        }
        else
        {
            return false;
        }


        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(item);
        }

        return true;
    }

    public void Remove(Item item)
    {
        //items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(item);
        }
    }

    private void Save(string temp)
    {
        SaveData.current.inventory = new InventoryData(gameObject.GetComponent<Inventory>());
    }

    public void Load(string temp)
    {
        /*InventoryData data = SaveData.current.inventory;

        if (data != default)
        {
            if(temp == "")
            {
                trinkets.Clear();
                abilities.Clear();
                seeds.Clear();

                foreach (string item in data.trinkets)
                {
                    trinkets.Add(Resources.Load("ScriptableObjects/Trinkets/" + item) as Item);

                }
                foreach (string item in data.abilities)
                {
                    abilities.Add(Resources.Load("ScriptableObjects/Abilities/" + item) as Item);
                }
                foreach (string item in data.seeds)
                {
                    seeds.Add(Resources.Load("ScriptableObjects/Seeds/" + item) as Item);
                }

                onInventoryLoadedCallback?.Invoke(trinkets, abilities, seeds, false);
            }
        }
        else
        {
            onInventoryLoadedCallback?.Invoke(trinkets, abilities, seeds, true);
        }*/
    }
    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}

[System.Serializable]
public class InventoryData
{
    public List<string> trinkets = new List<string>();
    public List<string> abilities = new List<string>();
    public List<string> seeds = new List<string>();

    public InventoryData(Inventory inventory)
    {
        foreach (Item item in inventory.trinkets)
        {
            trinkets.Add(item.name);
        }
        foreach (Item item in inventory.abilities)
        {
            abilities.Add(item.name);
        }
        foreach (Item item in inventory.seeds)
        {
            seeds.Add(item.name);
        }
    }
}