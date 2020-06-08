using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class DiceKey : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    private bool diceKeyAssigned = false;
    private bool isGold = false;
    private bool isPlatinum = false;
    private float diceNumber = -1;

    [SerializeField] GameObject selected;
    [SerializeField] GameObject assigned;

    public Sprite[] diceSpritesAssigned;
    private Animator animator;
    private Animator animatorAssigned;
    private AudioManager audioManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        animatorAssigned = GetComponentInChildren<Animator>();
        animator.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();
    }
    // Update is called once per frame
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

    public void SetAssignedStatus(bool status, int keyNumber)
    {
        if (!status)
        {
            diceKeyAssigned = false;
            assigned.SetActive(false);
            //animatorAssigned.enabled = false;
        }
        else
        {
            assigned.GetComponent<Image>().sprite = diceSpritesAssigned[keyNumber - 1];
            diceKeyAssigned = true;
            assigned.SetActive(true);
            //animatorAssigned.enabled = true;
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
        isGold = status;
        diceNumber = number;
        animator.SetBool("isGold", status);
        animator.SetFloat("diceNumber", diceNumber);
        if (status && diceNumber < 5)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    public void SetPlatinum(bool status, int diceNumber)
    {
        isPlatinum = status;
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
}
