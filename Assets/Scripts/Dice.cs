using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Dice : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;
   
    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    [SerializeField] GameObject marked;

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
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
        //marked.SetActive(false);
        selected.SetActive(false);
    }

    public void DiceClicked()
    {
        if (!marked.activeSelf)
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
        }
    }

    private void CheckButtonStatus()
    {

    }
}
