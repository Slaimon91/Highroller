using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTabHolder : MonoBehaviour
{
    private InventoryUI inventoryUI;
    private PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.InventoryLeft.performed += ctx => TabLeft();
        controls.Gameplay.InventoryRight.performed += ctx => TabRight();
        controls.Gameplay.Inventory.performed += ctx => TabLeft();
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    void TabLeft()
    {
        inventoryUI.ChangeInventoryTab(-1);
    }

    void TabRight()
    {
        inventoryUI.ChangeInventoryTab(1);
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
