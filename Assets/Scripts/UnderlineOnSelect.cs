using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UnderlineOnSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        TMP_Text text = GetComponentInChildren<TextMeshProUGUI>();
        text.fontStyle = FontStyles.Underline;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        TMP_Text text = GetComponentInChildren<TextMeshProUGUI>();
        text.fontStyle = FontStyles.Normal;
    }
}
