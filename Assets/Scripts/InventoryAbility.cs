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
    private bool created = false;

    [SerializeField] Image infoImage;
    [SerializeField] Sprite emptySprite;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;

    private string abilityName = "";
    private string abilityText = "";
    private Sprite abilitySprite;

    public void Created()
    {
        if (GetComponentInChildren<AbilityBase>() != null)
        {
            abilityName = GetComponentInChildren<AbilityBase>().GetAbilityName();
            abilityText = GetComponentInChildren<AbilityBase>().GetInfo();
            abilitySprite = GetComponentInChildren<AbilityBase>().GetBattleImageSprite();
            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(!created)
        {
            Created();
        }

        buttonSelected = true;
        selected.SetActive(true);

        if (created)
        {
            infoName.text = abilityName;
            infoText.text = abilityText;
            infoImage.sprite = abilitySprite;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
        infoName.text = "";
        infoText.text = "";
        infoImage.sprite = emptySprite;
    }

    public void ForceDeselect()
    {
        buttonSelected = false;
        selected.SetActive(false);
    }
}
