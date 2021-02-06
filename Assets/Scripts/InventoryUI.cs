using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Transform abilityItemsParent;
    public Transform seedItemsParent;
    public Transform trinketItemsParent;

    public bool inventoryFinishedLoading;

    Inventory inventory;

    private List<InventorySlot> seedSlots = new List<InventorySlot>();
    private Item equippedSeed;
    private List<InventorySlot> abilitySlots = new List<InventorySlot>();
    public List<InventorySlot> equippedAbilitySlots = new List<InventorySlot>();
    private List<InventorySlot> trinketSlots = new List<InventorySlot>();

    [SerializeField] List<InventoryTab> inventoryTabs = new List<InventoryTab>();
    private InventoryTab currentActiveTab;
    [SerializeField] GameObject inventoryInput;

    EventSystem eventSystem;
    PlayerController playerController;
    [SerializeField] PlayerValues playerValues;

    public delegate void OnInventoryFinishedLoading();
    public OnInventoryFinishedLoading onInventoryFinishedLoadingCallback;
    void Awake()
    {
        int GameStatusCount = FindObjectsOfType<InventoryUI>().Length;
        if (GameStatusCount > 1)
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else
        {
            //DontDestroyOnLoad(gameObject);
            DontDestroyOnLoadManager.DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        inventory = Inventory.instance;

        foreach (InventorySlot slot in abilityItemsParent.GetComponentsInChildren<InventorySlot>())
        {
            abilitySlots.Add(slot);
        }
        foreach (InventorySlot slot in seedItemsParent.GetComponentsInChildren<InventorySlot>())
        {
            seedSlots.Add(slot);
        }
        foreach (InventorySlot slot in trinketItemsParent.GetComponentsInChildren<InventorySlot>())
        {
            trinketSlots.Add(slot);
        }

        inventory.onItemChangedCallback += UpdateUI;
        inventory.onInventoryLoadedCallback += LoadUI;

        currentActiveTab = inventoryTabs[0];
        currentActiveTab.gameObject.SetActive(true);

        eventSystem = FindObjectOfType<EventSystem>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OpenInventory()
    {
        if (!inventoryInput.activeSelf)
        {
            currentActiveTab.gameObject.SetActive(true);
            inventoryInput.SetActive(!inventoryInput.activeSelf);
            if (eventSystem == null)
            {
                eventSystem = FindObjectOfType<EventSystem>();
            }
            InitiateItems();
            eventSystem.SetSelectedGameObject(currentActiveTab.GetFirstSlot());
            //playerController.SetGameState(GameState.PAUSED);
        }
    }

    public void CloseInventory()
    {
        if (inventoryInput.activeSelf)
        {
            DeselectAll();
            inventoryInput.SetActive(!inventoryInput.activeSelf);
            eventSystem.SetSelectedGameObject(null);
            if(playerController == null)
            {
                playerController = FindObjectOfType<PlayerController>();
            }
            playerController.SetGameState(GameState.PLAYING);
        }
    }

    public List<AbilityBase> GetPlayerAbilities()
    {
        List<AbilityBase> abilities = new List<AbilityBase>();

        foreach (InventorySlot slot in equippedAbilitySlots)
        {
            if(slot.item != null)
            {
                if (slot.item.prefab.GetComponent<AbilityBase>() != null)
                {
                    abilities.Add(slot.item.prefab.GetComponent<AbilityBase>());
                }
            }
        }
        return abilities;
    }

    void UpdateUI(Item item)
    {
        if (item.prefab.GetComponent<AbilityBase>() != null)
        {
            int slotNr = item.prefab.GetComponent<AbilityBase>().GetInventorySlotNr();
            abilitySlots[slotNr].AddItem(item);
        }
        if (item.prefab.GetComponent<SeedBase>() != null)
        {
            int slotNr = item.prefab.GetComponent<SeedBase>().GetInventorySlotNr();
            seedSlots[slotNr].AddItem(item);
        }
        if (item.prefab.GetComponent<TrinketBase>() != null)
        {
            int slotNr = item.prefab.GetComponent<TrinketBase>().GetInventorySlotNr();
            if(trinketSlots[slotNr].GetComponentInChildren<TrinketBase>() == null)
            {
                trinketSlots[slotNr].AddItem(item);
                trinketSlots[slotNr].GetComponentInChildren<InventoryTrinket>().ChangeTrinketCount(1);
            }
            else
            {
                trinketSlots[slotNr].GetComponentInChildren<InventoryTrinket>().ChangeTrinketCount(1);
            }
        }
    }

    void LoadUI(List<Item> trinkets, List<Item> abilities, List<KeyValuePair<Item, bool>> seeds, List<KeyValuePair<Item, bool>> seedsActiveStatus, List<KeyValuePair<Item, int>> equippedAbilities)
    {

        foreach (Item item in trinkets)
        {
            int slotNr = item.prefab.GetComponent<TrinketBase>().GetInventorySlotNr();
            if (trinketSlots[slotNr].GetComponentInChildren<TrinketBase>() == null)
            {
                trinketSlots[slotNr].AddItem(item);
                trinketSlots[slotNr].GetComponentInChildren<InventoryTrinket>().ChangeTrinketCount(1);
            }
            else
            {
                trinketSlots[slotNr].GetComponentInChildren<InventoryTrinket>().ChangeTrinketCount(1);
            }
        }
        foreach (Item item in abilities)
        {
            int slotNr = item.prefab.GetComponent<AbilityBase>().GetInventorySlotNr();
            abilitySlots[slotNr].AddItem(item);
        }
        foreach (KeyValuePair<Item, bool> item in seeds)
        {
            int slotNr = item.Key.prefab.GetComponent<SeedBase>().GetInventorySlotNr();
            seedSlots[slotNr].AddItem(item.Key);
            //item.Key.prefab.GetComponent<SeedBase>().SetBerryStatus(item.Value);
            seedSlots[slotNr].GetComponentInChildren<SeedBase>().SetBerryStatus(item.Value);
        }

        foreach (KeyValuePair<Item, bool> item in seedsActiveStatus)
        {
            int slotNr = item.Key.prefab.GetComponent<SeedBase>().GetInventorySlotNr();
            GetInventorySeed(seedSlots[slotNr].GetComponentInChildren<SeedBase>()).ToggleInactivateSlot(item.Value);
        }

        InitiateItems();

        foreach (KeyValuePair<Item, int> item in equippedAbilities)
        {
            EquipAbility(item.Key.prefab.GetComponent<AbilityBase>(), true, item.Value);
        }

        InitiateEquippedAbilities();

        onInventoryFinishedLoadingCallback?.Invoke();
        inventoryFinishedLoading = true;
    }

    public void ChangeInventoryTab(int tabValue)
    {
        DeselectAll();
        int index = inventoryTabs.IndexOf(currentActiveTab);

        currentActiveTab.gameObject.SetActive(false);
        index += tabValue;

        if (index <= -1)
        {
            index = inventoryTabs.Count - 1;
        }
        else if(index >= inventoryTabs.Count)
        {
            index = 0;
        }

        currentActiveTab = inventoryTabs[index];
        currentActiveTab.gameObject.SetActive(true);
        InitiateItems();
        if (currentActiveTab.GetFirstSlot() != null)
        {
            eventSystem.SetSelectedGameObject(currentActiveTab.GetFirstSlot());
        }
    }

    public void InitiateItems()
    {
        InventoryAbility[] abilities = FindObjectsOfType<InventoryAbility>();
        foreach (InventoryAbility ability in abilities)
        {
            ability.Created();
        }

        InventorySeed[] seeds = FindObjectsOfType<InventorySeed>();
        foreach (InventorySeed seed in seeds)
        {
            seed.Created();
        }

        InventoryTrinket[] trinkets = FindObjectsOfType<InventoryTrinket>();
        foreach (InventoryTrinket trinket in trinkets)
        {
            trinket.Created();
        }
    }

    public void InitiateEquippedAbilities()
    {

        //InventoryAbilityEquipped[] abilities;// = FindObjectsOfType<InventoryAbilityEquipped>();
        List<InventoryAbilityEquipped> abilities = new List<InventoryAbilityEquipped>();
        for(int i = 0; i < equippedAbilitySlots.Count; i++)
        {
            abilities.Add(equippedAbilitySlots[i].GetComponentInChildren<InventoryAbilityEquipped>());
        }
        foreach (InventoryAbilityEquipped ability in abilities)
        {
            
            ability.Created();
        }
    }

    public void InitiateEquippedSeed(SeedBase seedToEquip)
    {
        EquippedInventorySeed seed = FindObjectOfType<EquippedInventorySeed>();
        seed.Created(seedToEquip);
    }

    public void DeselectAll()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            var go = EventSystem.current.currentSelectedGameObject;

            if (go.GetComponent<InventoryAbility>() != null)
            {
                InventoryAbility ability = go.GetComponent<InventoryAbility>();
                ability.ForceDeselect();
            }
            if (go.GetComponent<InventorySeed>() != null)
            {
                InventorySeed seed = go.GetComponent<InventorySeed>();
                seed.ForceDeselect();
            }
            if (go.GetComponent<InventoryTrinket>() != null)
            {
                InventoryTrinket trinket = go.GetComponent<InventoryTrinket>();
                trinket.ForceDeselect();
            }
        }
    }

    public void EquipAbility(AbilityBase abilityToEquip, bool isOnLoad, int slotNr)
    {
        Item newItem = ScriptableObject.CreateInstance("Item") as Item;
        newItem.name = abilityToEquip.name;
        newItem.prefab = abilityToEquip.gameObject;

        if(isOnLoad)
        {
            for (int i = 0; i < equippedAbilitySlots.Count; i++)//(InventorySlot inventorySlot in equippedAbilitySlots)
            {
                if (equippedAbilitySlots[i].GetItem() == null && i == slotNr)
                {
                    equippedAbilitySlots[i].AddItem(newItem);
                    InitiateEquippedAbilities();
                    if (isOnLoad)
                    {
                        foreach (InventorySlot slot in abilitySlots)
                        {
                            if (slot.item != null)
                            {
                                if (slot.item.prefab.GetComponent<AbilityBase>() != null)
                                {
                                    if (abilityToEquip.GetAbilityName() == slot.item.prefab.GetComponent<AbilityBase>().GetAbilityName())
                                    {
                                        slot.GetComponentInChildren<InventoryAbility>().EquipFromLoad();
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
            }
        }
        else
        {
            foreach (InventorySlot inventorySlot in equippedAbilitySlots)
            {
                if (inventorySlot.GetItem() == null)
                {
                    inventorySlot.AddItem(newItem);
                    InitiateEquippedAbilities();
                    break;
                }
            }
        }
    }

    public void ClearAbilities()
    {
        if(currentActiveTab == inventoryTabs[1])
        {
            for (int i = 0; i < equippedAbilitySlots.Count; i++)
            {
                if (equippedAbilitySlots[i].GetItem() != null)
                {
                    equippedAbilitySlots[i].GetComponentInChildren<InventoryAbilityEquipped>().UnEquip();
                    //UnequipAbility(equippedAbilitySlots[i].GetItem().prefab.GetComponent<AbilityBase>().GetInventorySlotNr());
                }
            }
        }
    }

    public List<SeedBase> GetActiveSeeds()
    {
        List<SeedBase> seedList = new List<SeedBase>();

        foreach(InventorySlot i  in seedSlots)
        {
            SeedBase seed = i.GetChildHolder().GetComponentInChildren<SeedBase>();
            if(seed != null)
            {
                if(!seed.GetInactiveStatus() && !seed.GetComponent<SeedBase>().GetBerryStatus())
                {
                    seedList.Add(seed);
                }
            }
        }

        return seedList;
    }

    public SeedBase GetSeed(SeedBase seed)
    {
        foreach (InventorySlot i in seedSlots)
        {
            SeedBase seed2 = i.GetChildHolder().GetComponentInChildren<SeedBase>();
            if (seed2 != null)
            {
                if(seed.GetSeedName() == seed2.GetSeedName())
                {
                    return seed2;
                }
            }
        }

        return null;
    }

    public InventorySeed GetInventorySeed(SeedBase seed)
    {
        foreach (InventorySlot i in seedSlots)
        {
            SeedBase seed2 = i.GetChildHolder().GetComponentInChildren<SeedBase>();
            if (seed2 != null)
            {
                if (seed.GetSeedName() == seed2.GetSeedName())
                {
                    return i.GetChildHolder().GetComponentInChildren<InventorySeed>();
                }
            }
        }

        return null;
    }

    public void UnequipAbility(int abilityToUnequip)
    {
        abilitySlots[abilityToUnequip].GetComponentInChildren<InventoryAbility>().Unequip();
        
        foreach (InventorySlot inventorySlot in equippedAbilitySlots)
        {
            if(inventorySlot.GetItem() != null)
            {
                if (inventorySlot.GetItem().prefab.GetComponent<AbilityBase>().GetAbilityName() == abilitySlots[abilityToUnequip].GetItem().prefab.GetComponent<AbilityBase>().GetAbilityName())
                {

                    inventorySlot.ClearSlot();
                    break;
                }
            }
        }
    }

    /*public bool CheckForItem(Item searchItem)
{
    foreach (InventorySlot i in seedSlots)
    {
        SeedBase seed = i.GetChildHolder().GetComponentInChildren<SeedBase>();
        if (seed != null)
        {
            if (!seed.GetInactiveStatus() && !seed.GetComponent<SeedBase>().GetBerryStatus())
            {
                seedList.Add(seed);
            }
        }
    }
    InventoryTrinket[] trinkets = FindObjectsOfType<InventoryTrinket>();
    foreach (InventoryTrinket trinket in trinkets)
    {
        trinket.Created();
    }
}*/

}
