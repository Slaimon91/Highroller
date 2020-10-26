using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;
using System.Linq;

public class DiceKey : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    private bool diceKeyAssigned = false;
    private bool diceInactive = false;
    private bool isGold = false;
    private bool isPlatinum = false;
    private int diceNumber = -1;
    
    [SerializeField] GameObject selected;
    [SerializeField] GameObject assigned;
    [SerializeField] List<GameObject> moreDices = new List<GameObject>();
    [SerializeField] List<Sprite> emptyDiceImages = new List<Sprite>();
    private List<string> moreStatuses = new List<string>();
    private List<int> moreDiceNumbers = new List<int>();

    public Sprite[] diceSpritesAssigned;
    private Animator animator;
    private Animator animatorAssigned;
    public AudioManager audioManager;
    private BattleSystem battleSystem;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animatorAssigned = GetComponentInChildren<Animator>();
        animator.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();
        
    }
    void Start()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (animator.enabled == true)
        {
            animator.Play(0, -1, GetComponentInParent<DiceMasterAnimator>().myTime);
        }
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

    public bool TestMoreDices()
    {
        bool isEmpty = !moreDiceNumbers.Any();
        if (isEmpty)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetDKNumber(int DKNumber)
    {
        diceNumber = DKNumber;
    }

    public void SetAssignedStatus(bool status, int keyNumber)
    {
        if (!status)
        {
            diceKeyAssigned = false;
            assigned.SetActive(false);
        }
        else
        {
            assigned.GetComponent<Image>().sprite = diceSpritesAssigned[keyNumber - 1];
            diceKeyAssigned = true;
            assigned.SetActive(true);
        }
    }

    public void SetButtonNavigation(Button otherButton, string direction)
    {
        Navigation nav = GetComponent<Button>().navigation;
        switch(direction)
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
        if(status)
        {
            isPlatinum = !status;
            diceInactive = !status;
        }

        isGold = status;
        diceNumber = number;
        animator.SetBool("isGold", status);
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

    public void SetPlatinum(bool status, int number)
    {
        if(status)
        {
            isGold = !status;
            diceInactive = !status;
        }

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

    public void SetInactive(bool status, int number)
    {
        diceInactive = status;
        diceNumber = number;
        //animator.enabled = false;
    }

    public bool GetInactiveStatus()
    {
        return diceInactive;
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
        return diceKeyAssigned;
    }

    public void DiceKeyClicked()
    {

    }

    public int GetDKNumber()
    {
        return diceNumber;
    }

    public void SetupMoreDices(List<string> moTypes, List<int> moDiceNumbers)
    {
        for (int i = 0; i < moreDices.Count; i++)
        {
            moreDices[i].SetActive(false);
        }

        moreStatuses.Clear();
        moreDiceNumbers.Clear();

        for (int i = 0; i < moDiceNumbers.Count; i++)
        {
            moreDices[i].SetActive(true);
            moreStatuses.Add(moTypes[i]);
            moreDiceNumbers.Add(moDiceNumbers[i]);

            if(moTypes[i] == "normal")
            {
                moreDices[i].GetComponent<Image>().sprite = emptyDiceImages[0];
            }

            if (moTypes[i] == "gold")
            {
                moreDices[i].GetComponent<Image>().sprite = emptyDiceImages[1];
            }

            if (moTypes[i] == "plat")
            {
                moreDices[i].GetComponent<Image>().sprite = emptyDiceImages[2];
            }

            if (moTypes[i] == "deactivated")
            {
                moreDices[i].GetComponent<Image>().sprite = emptyDiceImages[3];
            }
        }
    }

    public void ActivateNextDK()
    {
        string status = moreStatuses[0];
        int number = moreDiceNumbers[0];

        battleSystem.ActivateNextDK(gameObject.GetComponent<DiceKey>(), status, number);

        moreStatuses.RemoveAt(0);
        moreDiceNumbers.RemoveAt(0);

        List<string> tempStatuses = new List<string>();
        foreach(string stat in moreStatuses)
        {
            tempStatuses.Add(stat);
        }
        List<int> tempNumbers = new List<int>();
        foreach (int num in moreDiceNumbers)
        {
            tempNumbers.Add(num);
        }

        SetupMoreDices(tempStatuses, tempNumbers);
    }
}
