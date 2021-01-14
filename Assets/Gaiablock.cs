using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaiablock : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject popUpBox;
    [SerializeField] GameObject overworldCanvas;
    [SerializeField] int gaiaAmount = 0;
    [SerializeField] PlayerValues playerValues;
    [SerializeField] Gaiablockade gaiaBlockade;
    private GameObject popup;
    [HideInInspector] public bool isCleared = false;
    [HideInInspector] public string id;
    private bool startedTalking;

    void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }
    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }
    public void Interact()
    {
        if (!startedTalking)
        {
            startedTalking = true;

            GetComponent<GenericTextTrigger>().onFinishedTextCallback += AskQuestion;
        }
    }

    public void AskQuestion()
    {
        GetComponent<GenericTextTrigger>().onFinishedTextCallback -= AskQuestion;
        string questionText = "Release <color=#84B724>" + gaiaAmount + "</color>";
        popup = Instantiate(popUpBox, overworldCanvas.transform);
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback += YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback += NoGaiaBlock;
        popup.GetComponent<PopupQuestion>().SetQuestionText(questionText);
        if (playerValues.gaia < gaiaAmount)
        {
            popup.GetComponent<PopupQuestion>().DisableYesButton();
        }
    }

    public void YesGaiaBlock()
    {
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback -= YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback -= NoGaiaBlock;
        FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(-gaiaAmount);
        gaiaBlockade.PaidGaiaBlock();
        isCleared = true;
        gameObject.SetActive(false);
        startedTalking = false;
    }

    public void NoGaiaBlock()
    {
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback -= YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback -= NoGaiaBlock;
        startedTalking = false;
    }
    private void Save(string temp)
    {
        SaveData.current.gaiablocks.Add(new GaiablockData(gameObject.GetComponent<Gaiablock>()));
    }

    public void Load(string temp)
    {
        GaiablockData data = SaveData.current.gaiablocks.Find(x => x.id == id);

        if (data != default)
        {
            id = data.id;
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
public class GaiablockData
{
    public string id;
    public bool isCleared;

    public GaiablockData(Gaiablock gaiablock)
    {
        id = gaiablock.id;
        isCleared = gaiablock.isCleared;
    }
}

