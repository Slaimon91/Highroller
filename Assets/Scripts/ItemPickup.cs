using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    [SerializeField] GameObject rewardbox;
    [SerializeField] Sprite rewardIcon;
    public bool persistentObject = false;
    [HideInInspector] public string id;
    [HideInInspector] public bool pickedUp = false;
    void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }
    public void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        string itemIntro = "You got";
        string itemText = item.name;
        GameObject popup = Instantiate(rewardbox, FindObjectOfType<RewardboxParent>().transform);
        popup.GetComponent<Rewardbox>().AssignInfo(itemIntro, itemText, rewardIcon);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(63f / 255f, 202f / 255f, 184f / 255f));
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp && !persistentObject)
        {
            gameObject.SetActive(false);
            pickedUp = true;
        }
    }
    private void Save(string temp)
    {
        SaveData.current.itemPickups.Add(new ItemPickupsData(gameObject.GetComponent<ItemPickup>()));

    }

    public void Load(string temp)
    {
        ItemPickupsData data = SaveData.current.itemPickups.Find(x => x.id == id);

        if (data != default)
        {
            persistentObject = data.persistentObject;
            pickedUp = data.pickedUp;

            if (!persistentObject && pickedUp)
            {
                gameObject.SetActive(false);
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
public class ItemPickupsData
{
    public string id;
    public bool persistentObject;
    public bool pickedUp;

    public ItemPickupsData(ItemPickup itemPickup)
    {
        id = itemPickup.id;
        persistentObject = itemPickup.persistentObject;
        pickedUp = itemPickup.pickedUp;
    }
}
