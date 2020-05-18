using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class HoldAssignButton : MonoBehaviour, ISelectHandler, IDeselectHandler//, IPointerDownHandler, IPointerUpHandler
{
    private bool buttonDown;
    private float buttonDownTimer;
    private bool holding = false;
    private bool assignButtonSelected = false;

    [SerializeField] private float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;
    //private GameObject thisButton;

    /*public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        Debug.Log("OnPointerUp");

    }*/

    void Start()
    {
       // thisButton = GetComponent<Button>().gameObject;
    }

    public void OnSelect(BaseEventData eventData)
    {
        /*if(eventData.selectedObject == thisButton)
        {
            Debug.Log("SELECTED BUTTON");
        }

        Debug.Log("UN BUTTON");*/
        assignButtonSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        assignButtonSelected = false;
        Reset();
    }

    public void HoldingButton()
    {
        buttonDown = true;
    }

    public void ReleasedButton()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if(assignButtonSelected)
        {
            CheckButtonStatus();
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
    }

    private void Reset()
    {
        buttonDown = false;
        buttonDownTimer = 0;
        fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
    }

    private void CheckButtonStatus()
    {
        if (CrossPlatformInputManager.GetButtonDown("Submit"))
        {
            holding = true;
            HoldingButton();
        }
        else if (CrossPlatformInputManager.GetButtonUp("Submit"))
        {
            if (holding)
            {
                holding = false;
                ReleasedButton();
            }
        }
    }
}
