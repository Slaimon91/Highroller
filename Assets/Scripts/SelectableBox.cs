using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableBox : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject selector;
    [SerializeField] GameObject selected;

    public void OnSelect(BaseEventData eventData)
    {
        selector.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selector.SetActive(false);
    }

    public void Selected()
    {
        selector.SetActive(false);
        if(selected != null)
            selected.SetActive(true);
    }

    public void Deselected()
    {
        selector.SetActive(true);
        if (selected != null)
            selected.SetActive(false);
    }

    public void DeselectedNoReselect()
    {
        if (selected != null)
            selected.SetActive(false);
    }
}
