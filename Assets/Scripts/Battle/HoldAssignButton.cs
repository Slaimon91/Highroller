﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class HoldAssignButton : MonoBehaviour
{
    private bool buttonDown;
    private float buttonDownTimer;
    private bool holding = false;
    private bool passCompleted = false;

    [SerializeField] private float requiredHoldTime;
    [SerializeField] private float completedWaitTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject completedImage;

    PlayerControls controls;
    private GameObject savedSelectedGameObject;
    private EventSystem eventSystem;
    private AudioManager audioManager;
    private Animator animatorPass;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Pass.performed += ctx => ButtonStarted();
        controls.Gameplay.Pass.canceled += ctx => ButtonCanceled();
        eventSystem = FindObjectOfType<EventSystem>();
        audioManager = FindObjectOfType<AudioManager>();
        animatorPass = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown)
        {
            if(eventSystem.currentSelectedGameObject != null)
            {
                savedSelectedGameObject = eventSystem.currentSelectedGameObject;
                audioManager.Play("Passing");
            }

            eventSystem.SetSelectedGameObject(null);
            
            buttonDownTimer += Time.deltaTime;
            if (buttonDownTimer >= requiredHoldTime)
            {
                if (passCompleted == false)
                {
                    passCompleted = true;
                    completedImage.SetActive(true);
                    fillImage.gameObject.SetActive(false);
                    PassCompleted();
                }
            }
            fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
        }
    }

    private void PassCompleted()
    {
        onLongClick.Invoke();
        audioManager.Stop("Passing");
        audioManager.Play("PassCompleted");
        savedSelectedGameObject = eventSystem.firstSelectedGameObject;
    }

    public void Reset()
    {
        audioManager.Stop("Passing");
        buttonDown = false;
        buttonDownTimer = 0;
        passCompleted = false;
        completedImage.SetActive(false);
        fillImage.gameObject.SetActive(true);
        fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
    }
    
    private void ButtonStarted()
    {
        holding = true;
        buttonDown = true;
    }

    private void ButtonCanceled()
    {
        if (holding && !passCompleted)
        {
            holding = false;
            Reset();
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
