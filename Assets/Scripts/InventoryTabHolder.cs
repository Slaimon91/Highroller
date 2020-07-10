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
        controls.InventoryUI.InventoryLeft.performed += ctx => TabLeft();
        controls.InventoryUI.InventoryRight.performed += ctx => TabRight();
        controls.InventoryUI.Inventory.performed += ctx => TabLeft();
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
        controls.InventoryUI.Enable();
    }

    void OnDisable()
    {
        controls.InventoryUI.Disable();
    }
}
