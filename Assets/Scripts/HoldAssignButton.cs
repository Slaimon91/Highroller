using System.Collections;
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

    [SerializeField] private float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;

    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Pass.performed += ctx => ButtonStarted();
        controls.Gameplay.Pass.canceled += ctx => ButtonCanceled();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown)
        {
            buttonDownTimer += Time.deltaTime;
            if (buttonDownTimer >= requiredHoldTime)
            {
                if (onLongClick != null)
                {
                    onLongClick.Invoke();
                }
                Reset();
            }
            fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
        }
    }

    private void Reset()
    {
        buttonDown = false;
        buttonDownTimer = 0;
        fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
    }
    
    private void ButtonStarted()
    {
        holding = true;
        buttonDown = true;
    }

    private void ButtonCanceled()
    {
        if (holding)
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
