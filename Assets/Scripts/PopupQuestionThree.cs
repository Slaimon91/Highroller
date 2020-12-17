using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PopupQuestionThree : MonoBehaviour
{
    [SerializeField] GameObject firstSlot;
    [SerializeField] Button yesButton;
    [SerializeField] GameObject yesOverlay;
    [SerializeField] TextMeshProUGUI questionText;
    EventSystem eventSystem;
    private GameObject savedSelectedGameObject;

    public delegate void OnYesAnswer();
    public OnYesAnswer onYesAnswerCallback;

    public delegate void OnMiddleAnswer();
    public OnMiddleAnswer onMiddleAnswerCallback;

    public delegate void OnNoAnswer();
    public OnNoAnswer onNoAnswerCallback;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void SetQuestionText(string questionTxt)
    {
        questionText.text = questionTxt;
    }
    void OnEnable()
    {
        savedSelectedGameObject = eventSystem.currentSelectedGameObject;
        StartCoroutine(delayedSelect());
        FindObjectOfType<PlayerControlsManager>().ToggleOnGenericUI();
    }

    IEnumerator delayedSelect()
    {
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(firstSlot);
    }

    public void NoPushed()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
        if (onNoAnswerCallback != null)
        {
            onNoAnswerCallback?.Invoke();
        }
        Destroy(gameObject);
    }

    public void MiddlePushed()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
        if (onMiddleAnswerCallback != null)
        {
            onMiddleAnswerCallback?.Invoke();
        }
        Destroy(gameObject);
    }

    public void YesPushed()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
        if (onYesAnswerCallback != null)
        {
            onYesAnswerCallback?.Invoke();
        }
        Destroy(gameObject);
    }

    public void DisableYesButton()
    {
        yesOverlay.SetActive(true);
        yesButton.enabled = false;
    }
}
