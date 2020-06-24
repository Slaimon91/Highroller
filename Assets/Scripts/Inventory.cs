using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of inventory");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged(Item item);
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();
    public List<Item> abilities = new List<Item>();

    public bool Add (Item item)
    {
        //if it is an ability
        if(item.prefab.GetComponent<AbilityBase>() != null)
        {
            //abilities.Add(item.prefab.GetComponent<AbilityBase>());
            abilities.Add(item);
        }
        else
        {
            return false;
        }

        //items.Add(item);

        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(item);
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(item);
        }
    }

}
