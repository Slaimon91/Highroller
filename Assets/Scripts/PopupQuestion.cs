using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PopupQuestion : MonoBehaviour
{
    [SerializeField] GameObject firstSlot;
    EventSystem eventSystem;
    private GameObject savedSelectedGameObject;

    public delegate void OnYesAnswer();
    public OnYesAnswer onYesAnswerCallback;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
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
        Destroy(gameObject);
    }

    public void YesPushed()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
        if (onYesAnswerCallback != null)
        {
            onYesAnswerCallback.Invoke();
        }
        Destroy(gameObject);
    }
}
