﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class BattleAbility : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;

    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    //[SerializeField] GameObject marked;

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
        //marked.SetActive(false);
        selected.SetActive(false);
    }

    public void AbilityClicked()
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
}