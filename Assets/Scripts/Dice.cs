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
    private BattleSystem battleSystem;
   
    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    [SerializeField] GameObject marked;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject inactive;
    [SerializeField] GameObject assigned;

    public Sprite[] diceSpritesLocked;
    public Sprite[] diceSpritesInactive;
    public Sprite[] diceSpritesAssigned;

    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.LockDice.performed += ctx => ToggleLockDice();
    }

    void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleLockDice()
    {
        if (buttonSelected)
        {
            if (!diceLocked && !diceInactive && !diceAssigned)
            {
                locked.GetComponent<Image>().sprite = diceSpritesLocked[battleSystem.GetDiceNumber(this) - 1];
                locked.SetActive(true);
                diceLocked = true;
            }
            else
            {
                locked.SetActive(false);
                diceLocked = false;
            }
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
            inactive.GetComponent<Image>().sprite = diceSpritesInactive[battleSystem.GetDiceNumber(this) - 1];
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
            assigned.GetComponent<Image>().sprite = diceSpritesAssigned[battleSystem.GetDiceNumber(this) - 1];
            diceAssigned = true;
            assigned.SetActive(true);
        }
    }

    public void SetButtonNavigation(Button otherButton, string direction)
    {
        Navigation nav = GetComponent<Button>().navigation;
        switch (direction)
        {
            case "up":
                nav.selectOnUp = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "down":
                nav.selectOnDown = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "left":
                nav.selectOnLeft = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "right":
                nav.selectOnRight = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
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

    public bool GetLockedStatus()
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
        if(!diceLocked && !diceInactive && !diceAssigned)
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
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
