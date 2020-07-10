using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class BattleAbilityHolder : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;

    [SerializeField] Sprite normal;
    [SerializeField] Sprite marked;
    [SerializeField] GameObject selected;
    [SerializeField] GameObject inactive;
    private GameObject battleImageHolder;
    private GameObject inventoryImageHolder;

    private GameObject infoTextImage;
    private TextMeshProUGUI infoName;
    private TextMeshProUGUI infoText;
    private string abilityName;
    private string abilityText;
    private bool activatable = false;
    private bool isMarked = false;
    Animator animator;

    void Start()
    {
        InfoPanel abilityPanel = GetComponentInParent<InfoPanel>();
        infoTextImage = abilityPanel.infoTextHolder;
        infoName = abilityPanel.infoName;
        infoText = abilityPanel.infoText;
        if(GetComponentInChildren<AbilityBase>() != null)
        {
            activatable = GetComponentInChildren<AbilityBase>().GetActivatableStatus();
            battleImageHolder = GetComponentInChildren<AbilityBase>().GetBattleImageHolder();
            inventoryImageHolder = GetComponentInChildren<AbilityBase>().GetInventoryImageHolder();
            battleImageHolder.GetComponent<Image>().enabled = true;
            inventoryImageHolder.GetComponent<Image>().enabled = false;
        }
        
        if(GetComponentInChildren<Animator>() != null)
        {
            animator = GetComponentInChildren<Animator>();
            animator.enabled = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);
        FindObjectOfType<AudioManager>().Play("MoveBattleCursor");
        infoName.text = abilityName;
        infoText.text = abilityText;
        infoTextImage.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
        infoTextImage.SetActive(false);
    }

    public void AbilityClicked()
    {
        if (!isMarked && activatable)
        {
            isMarked = true;
            battleImageHolder.GetComponent<Image>().sprite = marked;
            animator.SetBool("isMarked", true);
            //selected.SetActive(false);
        }
        else if (isMarked && activatable)
        {
            isMarked = false;
            battleImageHolder.GetComponent<Image>().sprite = normal;
            animator.SetBool("isMarked", false);
            //selected.SetActive(true);
        }
    }

    public void SetInactive()
    {
        isMarked = false;
        activatable = false;
        battleImageHolder.GetComponent<Image>().sprite = normal;
        inactive.SetActive(true);
        if(animator != null)
            animator.enabled = false;
    }

    public bool GetMarkedStatus()
    {
        return isMarked;
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

    public string GetAbilityName()
    {
        return abilityName;
    }

    public string GetAbilityText()
    {
        return abilityText;
    }

    public void SetAbilityName(string newName)
    {
        abilityName = newName;
    }

    public void SetAbilityText(string newText)
    {
        abilityText = newText;
    }
}
