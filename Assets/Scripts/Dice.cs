using System.Collections;
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
    private bool diceInactive = false;
    private bool diceMarked = false;
    private bool diceAssigned = false;

    private GameObject diceAssignedTo;
   
    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    [SerializeField] GameObject marked;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject inactive;
    [SerializeField] GameObject assigned;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(buttonSelected)
        {
            if (CrossPlatformInputManager.GetButtonDown("LockDice"))
            {
                ToggleLockDice();
            }
        }
    }

    public void ToggleLockDice()
    {
        if(!diceLocked && !diceInactive)
        {
            locked.SetActive(true);
            diceLocked = true;
        }
        else
        {
            locked.SetActive(false);
            diceLocked = false;
        }
    }

    public void SetInactiveStatus(bool inactiveStatus)
    {
        if(!inactiveStatus)
        {
            diceInactive = false;
            inactive.SetActive(false);
        }
        else
        {
            diceInactive = true;
            inactive.SetActive(true);
        }
    }

    public void SetMarkedStatus(bool markedStatus)
    {
        if(!markedStatus)
        {
            marked.SetActive(false);
            diceMarked = false;
        }
    }

    public void SetAssignedTo(GameObject enemyNumber)
    {
        diceAssignedTo = enemyNumber;
    }

    public void SetAssignedStatus(bool status)
    {
        if (!status)
        {
            diceAssigned = false;
            assigned.SetActive(false);
        }
        else
        {
            diceAssigned = true;
            assigned.SetActive(true);
        }
    }

    public bool GetAssignedStatus()
    {
        return diceAssigned;
    }

    public GameObject GetAssignedTo()
    {
        return diceAssignedTo;
    }

    public bool GetLockStatus()
    {
        return diceLocked;
    }

    public bool GetLockedOrInactiveStatus()
    {
        if (diceLocked || diceInactive)
        {
            return true;
        }
        return false;
    }

    public bool GetMarkedStatus()
    {
        return diceMarked;
    }

    public bool GetInactiveStatus()
    {
        return diceInactive;
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
    }

    public void DiceClicked()
    {
        if(!diceLocked && !diceInactive)
        {
            if (!marked.activeSelf)
            {
                marked.SetActive(true);
                diceMarked = true;
            }
            else
            {
                marked.SetActive(false);
                diceMarked = false;
            }
        }
        
    }
}
