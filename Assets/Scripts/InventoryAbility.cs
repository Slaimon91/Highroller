using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryAbility : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    [SerializeField] GameObject selected;
    [SerializeField] GameObject inactive;
    private bool isInactive = false;
    private bool created = false;

    [SerializeField] Image infoImage;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] Sprite yellowBox;
    [SerializeField] Sprite emptyBox;

    private string abilityName = "";
    private string abilityText = "";
    private Sprite abilitySprite;

    InventoryUI inventoryUI;

    void Awake()
    {
        abilitySprite = emptyBox;
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Created()
    {
        if(created)
        {
            return;
        }

        if (GetComponentInChildren<AbilityBase>() != null)
        {
            AbilityBase ability = GetComponentInChildren<AbilityBase>();

            abilityName = ability.GetAbilityName();
            abilityText = ability.GetInfo();
            abilitySprite = ability.GetInventoryImageSprite();

            if(ability.GetComponent<Animator>() != null)
            {
                ability.GetComponent<Animator>().enabled = false;
            }

            //ability.GetBattleImageHolder().GetComponent<Image>().sprite = yellowBox;

            ability.GetBattleImageHolder().GetComponent<Image>().enabled = false;
            ability.GetInventoryImageHolder().GetComponent<Image>().enabled = true;

            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);

        infoName.text = abilityName;
        infoText.text = abilityText;
        infoImage.sprite = abilitySprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
    }

    public void ForceDeselect()
    {
        buttonSelected = false;
        selected.SetActive(false);
    }

    public void Equip()
    {
        if(!isInactive)
        {
            if((inventoryUI = FindObjectOfType<InventoryUI>()) != null)
            {
                if(GetComponentInChildren<AbilityBase>() != null)
                {
                    inventoryUI.EquipAbility(GetComponentInChildren<AbilityBase>(), false, -1);
                    isInactive = true;
                    inactive.SetActive(true);
                }
            }
        }
    }

    public void EquipFromLoad()
    {
        if (!isInactive)
        {
            if ((inventoryUI = FindObjectOfType<InventoryUI>()) != null)
            {
                if (GetComponentInChildren<AbilityBase>() != null)
                {
                    isInactive = true;
                    inactive.SetActive(true);
                }
            }
        }
    }

    public void Unequip()
    {
        isInactive = false;
        inactive.SetActive(false);
    }
}
