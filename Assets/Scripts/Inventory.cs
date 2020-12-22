using System;
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
            Destroy(gameObject);
            return;
        }
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;

        gameObject.transform.parent = null;
        //DontDestroyOnLoad(gameObject);
        DontDestroyOnLoadManager.DontDestroyOnLoad(gameObject);

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

    public delegate void OnInventoryLoaded(List<Item> trinkets, List<Item> abilities, List<KeyValuePair<Item, bool>> seeds, List<KeyValuePair<Item, bool>> seedsActiveStatus, List<KeyValuePair<Item, int>> equippedAbilities);
    public OnInventoryLoaded onInventoryLoadedCallback;

    public List<Item> trinkets = new List<Item>();
    public List<Item> abilities = new List<Item>();
    public List<KeyValuePair<Item, bool>> seeds = new List<KeyValuePair<Item, bool>>();
    public List<KeyValuePair<Item, bool>> seedsActiveStatus = new List<KeyValuePair<Item, bool>>();
    //public List<Item> equippedAbilities = new List<Item>();
    public List<KeyValuePair<Item, int>> equippedAbilities = new List<KeyValuePair<Item, int>>();

    private InventoryUI inventoryUI;
    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
    public bool Add (Item item)
    {
        //if it is an ability
        if(item.prefab.GetComponent<AbilityBase>() != null)
        {
            abilities.Add(item);
        }
        //if it is a seed
        else if (item.prefab.GetComponent<SeedBase>() != null)
        {
            seeds.Add(new KeyValuePair<Item, bool>(item, false));
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
        SaveData.current.inventory = new InventoryData(gameObject.GetComponent<Inventory>(), inventoryUI);
    }

    public void Load(string temp)
    {
        InventoryData data = SaveData.current.inventory;

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
                foreach (KeyValuePair<string, bool> item in data.seeds)
                {
                    seeds.Add(new KeyValuePair<Item, bool>(Resources.Load("ScriptableObjects/Seeds/" + item.Key) as Item, item.Value));
                }
                foreach (KeyValuePair<string, bool> item in data.seedsActiveStatus)
                {
                    seedsActiveStatus.Add(new KeyValuePair<Item, bool>(Resources.Load("ScriptableObjects/Seeds/" + item.Key) as Item, item.Value));
                }
                foreach (KeyValuePair<string, int> item in data.equippedAbilities)
                {
                    equippedAbilities.Add(new KeyValuePair<Item, int>(Resources.Load("ScriptableObjects/Abilities/" + item.Key) as Item, item.Value));
                }

                onInventoryLoadedCallback?.Invoke(trinkets, abilities, seeds, seedsActiveStatus, equippedAbilities);
            }
        }
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
    public List<KeyValuePair<string, bool>> seeds = new List<KeyValuePair<string, bool>>();
    public List<KeyValuePair<string, bool>> seedsActiveStatus = new List<KeyValuePair<string, bool>>();
    public List<KeyValuePair<string, int>> equippedAbilities = new List<KeyValuePair<string, int>>();

    public InventoryData(Inventory inventory, InventoryUI inventoryUI)
    {
        foreach (Item item in inventory.trinkets)
        {
            trinkets.Add(item.name);
        }
        foreach (Item item in inventory.abilities)
        {
            abilities.Add(item.name);
        }
        foreach (KeyValuePair<Item, bool> item in inventory.seeds)
        {
            seeds.Add(new KeyValuePair<string, bool>(item.Key.name, inventoryUI.GetSeed(item.Key.prefab.GetComponent<SeedBase>()).GetBerryStatus()));
            seedsActiveStatus.Add(new KeyValuePair<string, bool>(item.Key.name, inventoryUI.GetInventorySeed(item.Key.prefab.GetComponent<SeedBase>()).GetStatus()));
        }
        foreach (InventorySlot slot in inventoryUI.equippedAbilitySlots)
        {
            if (slot.item != null)
            {
                if (slot.item.prefab.GetComponent<AbilityBase>() != null)
                {
                    equippedAbilities.Add(new KeyValuePair<string, int>(slot.item.prefab.GetComponent<AbilityBase>().GetAbilityName(), inventoryUI.equippedAbilitySlots.IndexOf(slot)));
                }
            }
        }
    }
}