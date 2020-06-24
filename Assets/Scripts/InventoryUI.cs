using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Transform abilityItemsParent;
    public GameObject firstSlot;

    Inventory inventory;

    private List<InventorySlot> seedSlots = new List<InventorySlot>();
    private List<InventorySlot> abilitySlots = new List<InventorySlot>();

    [SerializeField] List<GameObject> inventoryTabs = new List<GameObject>();
    private GameObject currentActiveTab;
    [SerializeField] GameObject inventoryInput;

    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Inventory.performed += ctx => ShowInventory();
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        foreach(InventorySlot slot in abilityItemsParent.GetComponentsInChildren<InventorySlot>())
        {
            abilitySlots.Add(slot);
        }
        currentActiveTab = inventoryTabs[0];
        currentActiveTab.SetActive(true);
        //inventoryInput = GetComponentInChildren<InventoryInput>().gameObject;
    }

    void ShowInventory()
    {
        if(!inventoryInput.activeSelf)
        {
            inventoryInput.SetActive(!inventoryInput.activeSelf);
            FindObjectOfType<EventSystem>().SetSelectedGameObject(firstSlot);
            FindObjectOfType<PlayerController>().SetGameState(GameState.PAUSED);
        }
        else
        {
            InventoryAbility[] abilities = FindObjectsOfType<InventoryAbility>();
            foreach(InventoryAbility ability in abilities)
            {
                ability.ForceDeselect();
            }

            inventoryInput.SetActive(!inventoryInput.activeSelf);
            FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
            FindObjectOfType<PlayerController>().SetGameState(GameState.PLAYING);
        }
        
    }

    void UpdateUI(Item item)
    {
        if (item.prefab.GetComponent<AbilityBase>() != null)
        {
            int slotNr = item.prefab.GetComponent<AbilityBase>().GetInventorySlotNr();
            abilitySlots[slotNr].AddItem(item);
            //abilitySlots[slotNr].GetChildHolder().GetComponent<InventoryAbility>().Created();
        }

        /*for (int i = 0; i < slots.Length; i++)
        {
            
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }*/
    }

    public void ChangeInventoryTab(int tabValue)
    {
        int index = inventoryTabs.IndexOf(currentActiveTab);

        currentActiveTab.SetActive(false);
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
        currentActiveTab.SetActive(true);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
