using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseOptions : MonoBehaviour
{
    [SerializeField] GameObject firstSlot;
    [SerializeField] GameObject pauseBackground;
    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    public void ClickContinue()
    {
        eventSystem.SetSelectedGameObject(null);
        FindObjectOfType<PlayerControlsManager>().CloseOptions();
        pauseBackground.SetActive(false);
    }

    public void ClickOptions()
    {

    }

    public void ClickQuit()
    {

    }

    public void OpenOptions()
    {
        pauseBackground.SetActive(true);
        eventSystem.SetSelectedGameObject(firstSlot);
    }
}
