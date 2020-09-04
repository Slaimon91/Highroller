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

}
