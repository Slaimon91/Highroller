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
    private bool passCompleted = false;

    [SerializeField] private float requiredHoldTime;
    [SerializeField] private float completedWaitTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject completedImage;

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
                if (onLongClick != null && passCompleted == false)
                {
                    onLongClick.Invoke();
                }
                if (passCompleted == false)
                {
                    passCompleted = true;
                    completedImage.SetActive(true);
                    StartCoroutine(PassCompleted());

                }


            }
            fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
        }
    }

    private IEnumerator PassCompleted()
    {
        yield return new WaitForSeconds(completedWaitTime);
        Reset();
    }

    private void Reset()
    {
        buttonDown = false;
        buttonDownTimer = 0;
        passCompleted = false;
        completedImage.SetActive(false);
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
