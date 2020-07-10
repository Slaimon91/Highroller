using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsManager : MonoBehaviour
{
    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        //playerInput.SwitchCurrentActionMap("Overworld");
    }

    public void ChangeToOverworld()
    {

        //playerInput.SwitchCurrentActionMap("Overworld");
        

    }
    public void ChangeToInventory()
    {
        //playerInput.SwitchCurrentActionMap("InventoryUI");
    }
    public void ChangeToBattle()
    {
        //playerInput.SwitchCurrentActionMap("Battle");
    }
    public void ChangeToDialogueOptions()
    {
        Debug.Log("HEj");
        //playerInput.SwitchCurrentActionMap("DialogueOptions");
        controls.InventoryUI.Disable();
        controls.Overworld.Disable();
        controls.Battle.Disable();
        controls.DialogueOptions.Enable();
    }

    public void Test()
    {
        Debug.Log("Hej");
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Overworld.Disable();
    }
}
