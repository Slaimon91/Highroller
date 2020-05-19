using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DiceKey : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;

    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;

    void Start()
    {
    }
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
        //marked.SetActive(false);
    }

    public void DiceKeyClicked()
    {
        /*if (!marked.activeSelf)
        {
            marked.SetActive(true);
            //  animator.SetBool("buttonMarked", true);
            //selected.SetActive(false);
        }
        else
        {
            marked.SetActive(false);
            //  animator.SetBool("buttonMarked", false);
            //selected.SetActive(true);
        }*/
    }

    private void CheckButtonStatus()
    {

    }
}
