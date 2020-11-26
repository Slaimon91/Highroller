using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] PlayerValues playerValues;
    public delegate void OnCancelGenericUI();
    public OnCancelGenericUI onCancelGenericUICallback;

    //OW
    private InventoryUI inventoryUI;
    private PlayerController playerController;
    private bool optionsOpen;

    //Battle
    private HoldAssignButton holdAssignButton;
    private BattleSystem battleSystem;
    private PlayerBattleController playerBattleController;
    private EventSystem eventSystem;
    private PauseOptions pauseOptions;

    private Vector2 movement;
    private bool[] savedControlStates  = { false, false, false};
    void Awake()
    {
        int GameStatusCount = FindObjectsOfType<PlayerControlsManager>().Length;
        if (GameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            gameObject.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }

        controls = new PlayerControls();

        controls.Overworld.Inventory.performed += ctx => TriggerOpenInventory();
        controls.Overworld.Interact.performed += ctx => TriggerInteract();
        controls.Overworld.Tileflip.performed += ctx => TriggerPressedTileFlip();
        controls.Overworld.ChangeSceneHax.performed += ctx => ChangeSceneToBattleHax();
        controls.Overworld.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Overworld.Move.canceled += ctx => movement = Vector2.zero;
        controls.Overworld.Options.performed += ctx => TriggerOptions();

        controls.GenericUI.Options.performed += ctx => TriggerOptions();
        controls.GenericUI.Cancel.performed += ctx => TriggerCancelGenericUI();

        controls.Overworld.HPHAX.performed += ctx => HPHAX();
        controls.Overworld.GAIAHAX.performed += ctx => GAIAHAX();
        controls.Overworld.MONEYHAX.performed += ctx => MONEYHAX();
        controls.Overworld.WATERHAX.performed += ctx => WATERHAX();
        controls.Overworld.BERRYHAX.performed += ctx => BERRYHAX();
        controls.Battle.KILLALL.performed += ctx => KILLALL();

        controls.InventoryUI.Inventory.performed += ctx => TriggerCloseInventory();
        controls.InventoryUI.InventoryLeft.performed += ctx => TriggerTabLeft();
        controls.InventoryUI.InventoryRight.performed += ctx => TriggerTabRight();


        controls.Battle.ChangeSceneHax.performed += ctx => ChangeSceneToOverworldHax();
        controls.Battle.Pass.performed += ctx => TriggerHoldButtonStarted();
        controls.Battle.Pass.canceled += ctx => TriggerHoldButtonCanceled();
        controls.Battle.Cancel.performed += ctx => TriggerPressedCancel();
        controls.Battle.Dodge.performed += ctx => TriggerDodgePushed();
        controls.Battle.Block.performed += ctx => TriggerBlockPushed();
        controls.Battle.LockDice.performed += ctx => TriggerToggleLockDice();
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

    private void HPHAX()
    {
        playerValues.healthPoints += 100;
    }

    private void GAIAHAX()
    {
        playerValues.gaia += 100;
    }

    private void MONEYHAX()
    {
        playerValues.currency += 100;
    }

    private void WATERHAX()
    {
        if (playerController == null)
        {
            if ((playerController = FindObjectOfType<PlayerController>()) == null)
            {
                return;
            }
        }

        playerController.ToggleWaterTransformation();
    }
    private void BERRYHAX()
    {
        playerValues.nrOfBattles++;
        if (FindObjectOfType<BerryTile>() != null)
        {
            List<BerryTile> berryTiles = new List<BerryTile>();
            berryTiles.AddRange(FindObjectsOfType<BerryTile>());
            foreach (BerryTile tile in berryTiles)
            {
                tile.ReloadBerries();
            }
        }
    }
    private void KILLALL()
    {
        if (battleSystem == null)
        {
            if ((battleSystem = FindObjectOfType<BattleSystem>()) == null)
            {
                return;
            }
        }

        battleSystem.KillAllEnemies();
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

    public void TriggerOptions()
    {
        if (controls.Overworld.enabled)
        {
            if (playerController == null)
            {
                if ((playerController = FindObjectOfType<PlayerController>()) == null)
                {
                    return;
                }
            }
            if (!playerController.interacting)
            {
                if (pauseOptions == null)
                {
                    if ((pauseOptions = FindObjectOfType<PauseOptions>()) == null)
                    {
                        return;
                    }
                }
                optionsOpen = true;
                pauseOptions.OpenOptions();
                ToggleOnGenericUI();
            }
        }
        else if(optionsOpen)
        {
            if (pauseOptions == null)
            {
                if ((pauseOptions = FindObjectOfType<PauseOptions>()) == null)
                {
                    return;
                }
            }

            pauseOptions.ClickContinue();
            CloseOptions();
        }
    }

    public void CloseOptions()
    {
        optionsOpen = false;
        ToggleOffGenericUI();
    }

    public void TriggerCancelGenericUI()
    {
        if (onCancelGenericUICallback != null)
        {
            onCancelGenericUICallback.Invoke();
        }
    }

    public void ChangeSceneToBattleHax()
    {
        ChangeToBattle();
        FindObjectOfType<LevelLoader>().LoadBattleScene();
    }
    public void ChangeSceneToOverworldHax()
    {
        ChangeToOverworld();
        FindObjectOfType<LevelLoader>().LoadOverworldScene();
    }

    public void TriggerHoldButtonStarted()
    {
        if (holdAssignButton == null)
        {
            if ((holdAssignButton = FindObjectOfType<HoldAssignButton>()) == null)
            {
                return;
            }
        }

        holdAssignButton.ButtonStarted();
    }

    public void TriggerHoldButtonCanceled()
    {
        if (holdAssignButton == null)
        {
            if ((holdAssignButton = FindObjectOfType<HoldAssignButton>()) == null)
            {
                return;
            }
        }

        holdAssignButton.ButtonCanceled();
    }

    public void TriggerPressedCancel()
    {
        if (battleSystem == null)
        {
            if ((battleSystem = FindObjectOfType<BattleSystem>()) == null)
            {
                return;
            }
        }

        battleSystem.PressedCancel();
    }

    public void TriggerDodgePushed()
    {
        if (playerBattleController == null)
        {
            if ((playerBattleController = FindObjectOfType<PlayerBattleController>()) == null)
            {
                return;
            }
        }

        playerBattleController.DodgePushed();
    }

    public void TriggerBlockPushed()
    {
        if (playerBattleController == null)
        {
            if ((playerBattleController = FindObjectOfType<PlayerBattleController>()) == null)
            {
                return;
            }
        }

        playerBattleController.BlockPushed();
    }

    public void TriggerToggleLockDice()
    {
        if (eventSystem == null)
        {
            if ((eventSystem = FindObjectOfType<EventSystem>()) == null)
            {
                return;
            }
        }

        if (eventSystem.currentSelectedGameObject != null)
        {
            if(eventSystem.currentSelectedGameObject.GetComponent<Dice>() != null)
            {
                eventSystem.currentSelectedGameObject.GetComponent<Dice>().ToggleLockDice();
            }
        }
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
        battleSystem = null;
        holdAssignButton = null;
        playerBattleController = null;
        eventSystem = null;
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
        inventoryUI = null;
        playerController = null;

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
      //  controls.Disable();
    }
}
