using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryAbilityEquipped : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    [SerializeField] GameObject selected;
    private bool created = false;

    [SerializeField] Image infoImage;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] Sprite yellowBox;
    [SerializeField] Sprite emptyBox;

    private string abilityName = "";
    private string abilityText = "";
    private Sprite abilitySprite;
    private AbilityBase ability;

    InventoryUI inventoryUI;

    void Awake()
    {

        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Created()
    {
        if (created)
        {
            return;
        }

        while(!created)
        {
            if(GetComponentInChildren<AbilityBase>() == null)
            {
                return;
            }

            ability = GetComponentInChildren<AbilityBase>();

            abilityName = ability.GetAbilityName();
            abilityText = ability.GetInfo();
            abilitySprite = ability.GetInventoryImageSprite();

            if (ability.GetComponent<Animator>() != null)
            {
                ability.GetComponent<Animator>().enabled = false;
            }

            //ability.GetBattleImageHolder().GetComponent<Image>().sprite = yellowBox;

            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(!created)
            abilitySprite = emptyBox;
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

    public void UnEquip()
    {
        if(created)
        {
            inventoryUI.UnequipAbility(ability.GetInventorySlotNr());
            Destroy(ability.gameObject);
            abilityName = "";
            abilityText = "";
            abilitySprite = emptyBox;
            created = false;
        }
    }
}
