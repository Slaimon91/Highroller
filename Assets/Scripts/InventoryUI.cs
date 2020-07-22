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

    Inventory inventory;

    private List<InventorySlot> seedSlots = new List<InventorySlot>();
    private Item equippedSeed;
    private List<InventorySlot> abilitySlots = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> equippedAbilitySlots = new List<InventorySlot>();
    private List<InventorySlot> trinketSlots = new List<InventorySlot>();

    [SerializeField] List<InventoryTab> inventoryTabs = new List<InventoryTab>();
    private InventoryTab currentActiveTab;
    [SerializeField] GameObject inventoryInput;

    EventSystem eventSystem;
    PlayerController playerController;
    [SerializeField] PlayerValues playerValues;
    void Awake()
    {
        int GameStatusCount = FindObjectsOfType<InventoryUI>().Length;
        if (GameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        foreach(InventorySlot slot in abilityItemsParent.GetComponentsInChildren<InventorySlot>())
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
            eventSystem.SetSelectedGameObject(currentActiveTab.GetFirstSlot());
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            //playerController.SetGameState(GameState.PAUSED);
            InitiateItems();            
        }
    }

    public void CloseInventory()
    {
        if (inventoryInput.activeSelf)
        {
            DeselectAll();
            inventoryInput.SetActive(!inventoryInput.activeSelf);
            eventSystem.SetSelectedGameObject(null);
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
            trinketSlots[trinketSlots.Count - 1].AddItem(item);
        }
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
        if(currentActiveTab.GetFirstSlot() != null)
        {
            eventSystem.SetSelectedGameObject(currentActiveTab.GetFirstSlot());
        }

        InitiateItems();
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
        InventoryAbilityEquipped[] abilities = FindObjectsOfType<InventoryAbilityEquipped>();
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

    public void EquipAbility(AbilityBase abilityToEquip)
    {
        Item newItem = ScriptableObject.CreateInstance("Item") as Item;
        newItem.name = abilityToEquip.name;
        newItem.prefab = abilityToEquip.gameObject;

        foreach(InventorySlot inventorySlot in equippedAbilitySlots)
        {
            if(inventorySlot.GetItem() == null)
            {
                inventorySlot.AddItem(newItem);
                InitiateEquippedAbilities();
                break;
            }
        }
    }

    public void EquipSeed(SeedBase seedToEquip)
    {
        InventorySeed[] seeds = FindObjectsOfType<InventorySeed>();
        foreach (InventorySeed seed in seeds)
        {
            seed.Unequip();
        }

        Item newItem = ScriptableObject.CreateInstance("Item") as Item;
        newItem.name = seedToEquip.name;
        newItem.prefab = seedToEquip.gameObject;

        equippedSeed = newItem;
        //equippedSeedSlot.AddItem(newItem);
        InitiateEquippedSeed(seedToEquip);
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
}
