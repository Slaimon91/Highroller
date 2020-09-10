using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TileflipRewardbox : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI desc;
    EventSystem eventSystem;
    private GameObject savedSelectedGameObject;

    public delegate void AcceptReward();
    public AcceptReward onAcceptRewardCallback;
    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    void OnEnable()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOnGenericUI();
        StartCoroutine(delayedSelect());
    }

    IEnumerator delayedSelect()
    {
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(gameObject);
    }

    public void AssignInfo(string infoText, Sprite infoSprite)
    {
        desc.text = infoText;
        icon.sprite = infoSprite;
    }

    public void ButtonPushed()
    {
        FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();

        if (onAcceptRewardCallback != null)
        {
            onAcceptRewardCallback.Invoke();
        }
        Destroy(gameObject);
    }
}
