using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class HoldAssignButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    [SerializeField] private float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        Debug.Log("OnPointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        Debug.Log("OnPointerUp");

    }

    public void HoldingButton()
    {
        pointerDown = true;
        Debug.Log("OnPointerDown333");
    }

    public void ReleasedButton()
    {
        Reset();
        Debug.Log("OnPointerUp333");
    }

    // Update is called once per frame
    void Update()
    {
        if(pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer >= requiredHoldTime)
            {
                if(onLongClick != null)
                {
                    onLongClick.Invoke();
                }
                Reset();
            }
            fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
}
