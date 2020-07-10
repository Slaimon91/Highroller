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
    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    void OnEnable()
    {
        Debug.Log(eventSystem.currentSelectedGameObject);
        savedSelectedGameObject = eventSystem.currentSelectedGameObject;
        eventSystem.SetSelectedGameObject(firstSlot);
        Debug.Log(eventSystem.currentSelectedGameObject);
        FindObjectOfType<PlayerControlsManager>().ChangeToDialogueOptions();
    }

    public void NoPushed()
    {

    }

    public void YesPushed()
    {

    }
}
