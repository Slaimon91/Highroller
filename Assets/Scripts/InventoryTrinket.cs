using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryTrinket : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    [SerializeField] GameObject selected;
    private bool created = false;

    [SerializeField] TextMeshProUGUI trinketInfoName;
    [SerializeField] TextMeshProUGUI trinketInfoText;

    private string trinketName = "";
    private string trinketText = "";

    public void Created()
    {
        if (created)
        {
            return;
        }

        if (GetComponentInChildren<TrinketBase>() != null)
        {
            TrinketBase item = GetComponentInChildren<TrinketBase>();

            trinketName = item.GetTrinketName();
            trinketText = item.GetInfo();

            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);

        trinketInfoName.text = trinketName;
        trinketInfoText.text = trinketText;
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
}
