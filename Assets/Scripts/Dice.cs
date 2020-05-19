﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Dice : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    private bool diceLocked = false;
   
    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    [SerializeField] GameObject marked;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject greyed; 

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(buttonSelected)
        {
            if (CrossPlatformInputManager.GetButtonDown("Tileflip"))
            {
                LockDice();
            }
        }
    }

    private void LockDice()
    {
        diceLocked = !diceLocked;

        if(diceLocked)
        {
            locked.SetActive(true);
        }
        else
        {
            locked.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
        //marked.SetActive(false);
    }

    public void DiceClicked()
    {
        if (!marked.activeSelf)
        {
            marked.SetActive(true);
          //  animator.SetBool("buttonMarked", true);
            //selected.SetActive(false);
        }
        else
        {
            marked.SetActive(false);
          //  animator.SetBool("buttonMarked", false);
            //selected.SetActive(true);
        }
    }

    private void CheckButtonStatus()
    {

    }
}
