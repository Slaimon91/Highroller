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

    [SerializeField] GameObject selected;
    [SerializeField] GameObject assigned;

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        selected.SetActive(false);
    }

    public void SetAssignedStatus(bool status)
    {
        if (!status)
        {
            diceKeyAssigned = false;
            assigned.SetActive(false);
        }
        else
        {
            diceKeyAssigned = true;
            assigned.SetActive(true);
        }
    }

    public bool GetAssignedStatus()
    {
        return diceKeyAssigned;
    }

    public void DiceKeyClicked()
    {

    }
}
