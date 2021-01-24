using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject savedSelectedGameObject;
    private EventSystem eventSystem;
    private AudioManager audioManager;
    private Animator animatorPass;
    private ButtonPanel buttonPanel;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        audioManager = FindObjectOfType<AudioManager>();
        animatorPass = GetComponent<Animator>();
    }

    void Start()
    {
        buttonPanel = FindObjectOfType<ButtonPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown && !passCompleted)
        {
            if(eventSystem.currentSelectedGameObject != null)
            {
                savedSelectedGameObject = eventSystem.currentSelectedGameObject;
                audioManager.Play("Passing");
            }

            eventSystem.SetSelectedGameObject(null);
            buttonPanel.SetEmptyRedText();
            buttonPanel.SetEmptyGreenText();
            buttonPanel.SetEmptyBlueText();

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
        else if(!passCompleted)
        {
            if(buttonDownTimer > 0)
            {
                buttonDownTimer -= Time.deltaTime;
                if (buttonDownTimer < 0)
                    buttonDownTimer = 0;
                fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
            }
        }
    }

    private void PassCompleted()
    {
        onLongClick.Invoke();
        audioManager.Stop("Passing");
        audioManager.Play("PassCompleted");
        buttonDown = false;
        savedSelectedGameObject = eventSystem.firstSelectedGameObject;
    }

    public void Reset()
    {
        audioManager.Stop("Passing");
        buttonDown = false;
        passCompleted = false;
        completedImage.SetActive(false);
        fillImage.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
    }
    
    public void ButtonStarted()
    {
        holding = true;
        buttonDown = true;
    }

    public void ButtonCanceled()
    {
        if (holding && !passCompleted)
        {
            holding = false;
            Reset();
        }
    }
}
