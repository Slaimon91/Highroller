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
    private bool isGold = false;
    private bool isPlatinum = false;
    private float diceNumber = -1;

    private GameObject diceAssignedTo;
    private BattleSystem battleSystem;
    private Animator animator;
    private AudioManager audioManager;
   
    [SerializeField] GameObject selected;
    [SerializeField] GameObject marked;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject inactive;
    [SerializeField] GameObject assigned;

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
        animator = GetComponent<Animator>();
        animator.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(animator.enabled == true)
        {
            animator.Play(0, -1, GetComponentInParent<DiceMasterAnimator>().myTime);
        }
    }

    public void ToggleLockDice()
    {
        if (buttonSelected)
        {
            if (!diceLocked && !diceInactive && !diceAssigned)
            {
                locked.SetActive(true);
                diceLocked = true;
                audioManager.Play("Lock");
                SetMarkedStatus(false);
            }
            else
            {
                locked.SetActive(false);
                diceLocked = false;
                audioManager.Stop("Lock");
            }
        }
    }

    public void UnlockDice()
    {
        locked.SetActive(false);
        diceLocked = false;
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
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

    public void SetGold(bool status, int number)
    {
        isGold = status;
        diceNumber = number;
        animator.SetBool("isGold", status);
        animator.SetFloat("diceNumber", diceNumber);
        if(status && diceNumber >= 2)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    public void SetPlatinum(bool status, int number)
    {
        isPlatinum = status;
        diceNumber = number;
        animator.SetBool("isPlatinum", status);
        animator.SetFloat("diceNumber", diceNumber);
        if (status && diceNumber >= 4)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    public bool GetGoldStatus()
    {
        return isGold;
    }

    public bool GetPlatinumStatus()
    {
        return isPlatinum;
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
        audioManager.Play("MoveBattleCursor");
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
