using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryTrinket : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
    [SerializeField] GameObject selected;
    [SerializeField] GameObject itemCount;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] GameObject selectedMultiple;
    private bool created = false;

    [SerializeField] TextMeshProUGUI trinketInfoName;
    [SerializeField] TextMeshProUGUI trinketInfoText;

    private string trinketName = "";
    private string trinketText = "";
    private int nrOfItems = 0;

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
            GetComponent<Image>().enabled = false;
            created = true;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(nrOfItems > 1)
        {
            buttonSelected = true;
            selectedMultiple.SetActive(true);
        }
        else
        {
            buttonSelected = true;
            selected.SetActive(true);
        }

        trinketInfoName.text = trinketName;
        trinketInfoText.text = trinketText;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
        selectedMultiple.SetActive(false);
    }

    public void ForceDeselect()
    {
        buttonSelected = false;
        selected.SetActive(false);
        itemCount.SetActive(false);
        selectedMultiple.SetActive(false);
    }

    public void ChangeTrinketCount(int nr)
    {
        nrOfItems += nr;

        if(nrOfItems > 1)
        {
            itemCount.SetActive(true);
            numberText.text = nrOfItems.ToString();
        }
        else
        {
            itemCount.SetActive(false);
        }
    }
}
