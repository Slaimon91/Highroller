using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    [SerializeField] GameObject rewardbox;
    [SerializeField] GameObject rewardboxHolder;
    [SerializeField] Sprite rewardIcon;
    [SerializeField] bool persistentObject = false;
    public void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        string itemIntro = "You got";
        string itemText = item.name;
        GameObject popup = Instantiate(rewardbox, rewardboxHolder.transform);
        popup.GetComponent<Rewardbox>().AssignInfo(itemIntro, itemText, rewardIcon);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(63f / 255f, 202f / 255f, 184f / 255f));
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp && !persistentObject)
        {
            Destroy(gameObject);
        }
    }
}
