using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    private PlayerControls controls;
    private InventoryUI inventoryUI;
    private PlayerController playerController;
    private Vector2 movement;
    private bool[] savedControlStates  = { false, false, false};
    void Awake()
    {
        controls = new PlayerControls();

        controls.Overworld.Inventory.performed += ctx => TriggerOpenInventory();
        controls.Overworld.Interact.performed += ctx => TriggerInteract();
        controls.Overworld.Tileflip.performed += ctx => TriggerPressedTileFlip();
        controls.Overworld.ChangeSceneHax.performed += ctx => ChangeSceneHax();
        controls.Overworld.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Overworld.Move.canceled += ctx => movement = Vector2.zero;

        controls.InventoryUI.Inventory.performed += ctx => TriggerCloseInventory();
        controls.InventoryUI.InventoryLeft.performed += ctx => TriggerTabLeft();
        controls.InventoryUI.InventoryRight.performed += ctx => TriggerTabRight();


        controls.Battle.ChangeSceneHax.performed += ctx => ChangeSceneHax();
    }

    void Update()
    {
        if(controls.Overworld.enabled)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (playerController != null)
        {
            if (movement != playerController.move)
            {
                playerController.move = movement;
            }
        }
        else
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }
    }

    public void TriggerOpenInventory()
    {
        if (inventoryUI == null)
        {
            if ((inventoryUI = FindObjectOfType<InventoryUI>()) == null)
            {
                return;
            }
        }
        ChangeToInventory();
        inventoryUI.OpenInventory();
    }

    public void TriggerCloseInventory()
    {
        if (inventoryUI == null)
        {
            if((inventoryUI = FindObjectOfType<InventoryUI>()) == null)
            {
                return;
            }
        }
        ChangeToOverworld();
        inventoryUI.CloseInventory();
        
    }

    public void TriggerTabLeft()
    {
        if (inventoryUI == null)
        {
            if ((inventoryUI = FindObjectOfType<InventoryUI>()) == null)
            {
                return;
            }
        }
       

        inventoryUI.ChangeInventoryTab(-1);
    }

    public void TriggerTabRight()
    {
        if (inventoryUI == null)
        {
            if ((inventoryUI = FindObjectOfType<InventoryUI>()) == null)
            {
                return;
            }
        }

        inventoryUI.ChangeInventoryTab(1);
    }

    public void TriggerInteract()
    {
        if (playerController == null)
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }

        playerController.Interact();
    }

    public void TriggerPressedTileFlip()
    {
        if (playerController == null)
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }

        playerController.PressedTileFlip();
    }

    public void ChangeSceneHax()
    {
        FindObjectOfType<LevelLoader>().LoadBattleScene();
    }

    public void ChangeToOverworld()
    {
        controls.InventoryUI.Disable();
        controls.Overworld.Enable();
        controls.Battle.Disable();
        controls.GenericUI.Disable();
        if (playerController == null)
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }
        playerController.SetGameState(GameState.PLAYING);
    }
    public void ChangeToInventory()
    {
        controls.InventoryUI.Enable();
        controls.Overworld.Disable();
        controls.Battle.Disable();
        controls.GenericUI.Disable();
        if (playerController == null)
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }
        playerController.SetGameState(GameState.PAUSED);
    }
    public void ChangeToBattle()
    {
        controls.InventoryUI.Disable();
        controls.Overworld.Disable();
        controls.Battle.Enable();
        controls.GenericUI.Disable();
    }
    public void ToggleOnGenericUI()
    {
        if(controls.InventoryUI.enabled)
        {
            savedControlStates[0] = true;
        }
        if (controls.Overworld.enabled)
        {
            savedControlStates[1] = true;
        }
        if (controls.Battle.enabled)
        {
            savedControlStates[2] = true;
        }

        controls.InventoryUI.Disable();
        controls.Overworld.Disable();
        controls.Battle.Disable();
        controls.GenericUI.Enable();
    }

    public void ToggleOffGenericUI()
    {
        if (savedControlStates[0] == true)
        {
            controls.InventoryUI.Enable();
        }
        if (savedControlStates[1] == true)
        {
            controls.Overworld.Enable();
        }
        if (savedControlStates[2] == true)
        {
            controls.Battle.Enable();
        }
        savedControlStates[0] = false;
        savedControlStates[1] = false;
        savedControlStates[2] = false;
        controls.GenericUI.Disable();
    }

    public PlayerControls GetControls()
    {
        return controls;
    }

    void OnEnable()
    {
        controls.InventoryUI.Disable();
        controls.Overworld.Enable();
        controls.Battle.Disable();
        controls.GenericUI.Disable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
