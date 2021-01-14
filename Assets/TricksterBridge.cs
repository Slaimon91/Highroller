using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterBridge : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject popUpBox;
    [SerializeField] GameObject overworldCanvas;
    [SerializeField] int goldenAcornAmount = 0;
    [SerializeField] PlayerValues playerValues;
    private GameObject popupGO;
    [HideInInspector] public bool isCleared = false;
    [SerializeField] Sprite background;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    private bool startedTalking = false;
    private PopupQuestionThree popup;

    void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    public void Interact()
    {
        if(!startedTalking)
        {
            startedTalking = true;

            GetComponent<DialogueTrigger>().onFinishedDialogueCallback += AskQuestion;
        }
    }
    public void AskQuestion()
    {
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback -= AskQuestion;
        string questionText = "Pay <color=#FFA81F>" + goldenAcornAmount + "</color>";
        popupGO = Instantiate(popUpBox, overworldCanvas.transform);
        popup = popupGO.GetComponent<PopupQuestionThree>();
        popup.onYesAnswerCallback += YesPay;
        popup.onMiddleAnswerCallback += NoFight;
        popup.onNoAnswerCallback += NoCancel;
        popup.SetQuestionText(questionText);
        if (playerValues.currency < goldenAcornAmount)
        {
            popup.DisableYesButton();
        }
    }
    public void YesPay()
    {
        popup.onYesAnswerCallback -= YesPay;
        popup.onMiddleAnswerCallback -= NoFight;
        popup.onNoAnswerCallback -= NoCancel;
        GetComponent<DialogueTrigger>().GoToDialogue(3);
        GetComponent<DialogueTrigger>().Interact();
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback += Leave;
    }

    public void Leave()
    {
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback -= Leave;
        FindObjectOfType<LaunchRewards>().LanuchGARewardbox(-goldenAcornAmount);
        isCleared = true;
        gameObject.SetActive(false);
    }

    private void StartBattle()
    {
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback -= StartBattle;
        StartCoroutine(FindObjectOfType<EncounterManager>().LaunchCustomBattle(null, enemies, background));
    }

    public void NoFight()
    {
        popup.onYesAnswerCallback -= YesPay;
        popup.onMiddleAnswerCallback -= NoFight;
        popup.onNoAnswerCallback -= NoCancel;
        isCleared = true;
        GetComponent<DialogueTrigger>().GoToDialogue(2);
        GetComponent<DialogueTrigger>().Interact();
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback += StartBattle;
    }

    public void NoCancel()
    {
        popup.onYesAnswerCallback -= YesPay;
        popup.onMiddleAnswerCallback -= NoFight;
        popup.onNoAnswerCallback -= NoCancel;

        GetComponent<DialogueTrigger>().GoToDialogue(1);
        GetComponent<DialogueTrigger>().Interact();
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback += ResetNoCancel;
    }

    private void ResetNoCancel()
    {
        startedTalking = false;
        GetComponent<DialogueTrigger>().GoToDialogue(0);
        GetComponent<DialogueTrigger>().onFinishedDialogueCallback -= ResetNoCancel;
    }
    private void Save(string temp)
    {
        SaveData.current.tricksterBridge = new TricksterBridgeData(gameObject.GetComponent<TricksterBridge>());
    }

    public void Load(string temp)
    {
        TricksterBridgeData data = SaveData.current.tricksterBridge;

        if (data != default)
        {
            isCleared = data.isCleared;

            if (isCleared)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}

[System.Serializable]
public class TricksterBridgeData
{
    public bool isCleared;

    public TricksterBridgeData(TricksterBridge tricksterBridge)
    {
        isCleared = tricksterBridge.isCleared;
    }
}

