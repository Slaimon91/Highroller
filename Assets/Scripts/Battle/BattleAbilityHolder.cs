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
    private GameObject imageHolder;

    private GameObject infoTextImage;
    private TextMeshProUGUI infoName;
    private TextMeshProUGUI infoText;
    [HideInInspector] public string unitName;
    [HideInInspector] public string unitText;
    private bool activatable = false;
    private bool isMarked = false;
    Animator animator;

    void Start()
    {
        InfoPanel abilityPanel = GetComponentInParent<InfoPanel>();
        infoTextImage = abilityPanel.infoTextHolder;
        infoName = abilityPanel.infoName;
        infoText = abilityPanel.infoText;
        activatable = GetComponentInChildren<AbilityBase>().GetActivatableStatus();
        imageHolder = GetComponentInChildren<AbilityBase>().GetBattleImageHolder();
        animator = GetComponentInChildren<Animator>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);
        FindObjectOfType<AudioManager>().Play("MoveBattleCursor");
        infoName.text = unitName;
        infoText.text = unitText;
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
            imageHolder.GetComponent<Image>().sprite = marked;
            animator.SetBool("isMarked", true);
            //selected.SetActive(false);
        }
        else if (isMarked && activatable)
        {
            isMarked = false;
            imageHolder.GetComponent<Image>().sprite = normal;
            animator.SetBool("isMarked", false);
            //selected.SetActive(true);
        }
    }

    public void SetInactive()
    {
        isMarked = false;
        activatable = false;
        imageHolder.GetComponent<Image>().sprite = normal;
        inactive.SetActive(true);
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
}
