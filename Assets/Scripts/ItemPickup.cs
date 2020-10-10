﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    [SerializeField] GameObject rewardbox;
    [SerializeField] GameObject rewardboxHolder;
    public void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        string itemIntro = "You got";
        string itemText = item.name;
        GameObject popup = Instantiate(rewardbox, rewardboxHolder.transform);
        popup.GetComponent<Rewardbox>().AssignInfo(itemIntro, itemText, gameObject.GetComponent<SpriteRenderer>().sprite);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(255f / 255f, 164f / 255f, 59f / 255f));
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
