using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public List<Dialogue> dialogues = new List<Dialogue>();
    private PlayerController playerController;
    [HideInInspector] public int dialogueNr = 0;
    [HideInInspector] public string id;
    public delegate void OnFinishedDialogue();
    public OnFinishedDialogue onFinishedDialogueCallback;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
    }

    void Start()
    {
        id = GetComponent<UniqueID>().id;
    }

    public void Interact()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues[dialogueNr]);
        playerController.onFinishedInteractingCallback += DialogueFinished;
    }

    private void DialogueFinished()
    {
        playerController.onFinishedInteractingCallback -= DialogueFinished;
        if (onFinishedDialogueCallback != null)
        {
            onFinishedDialogueCallback?.Invoke();
        }
    }

    public void AdvanceDialogue()
    {
        dialogueNr++;
    }

    public void GoToDialogue(int nr)
    {
        dialogueNr = nr;
    }
    private void Save(string temp)
    {
        SaveData.current.dialogueTriggers.Add(new DialogueTriggerData(gameObject.GetComponent<DialogueTrigger>()));

    }

    public void Load(string temp)
    {
        DialogueTriggerData data = SaveData.current.dialogueTriggers.Find(x => x.id == id);


        if (data != default)
        {
            dialogueNr = data.dialogueNr;
        }
    }

    public void OnDestroy()
    {
        GameEvents.SaveInitiated -= Save;
        GameEvents.LoadInitiated -= Load;
    }
}

[System.Serializable]
public class DialogueTriggerData
{
    public string id;
    public int dialogueNr;

    public DialogueTriggerData(DialogueTrigger dialogueTrigger)
    {
        id = dialogueTrigger.id;
        dialogueNr = dialogueTrigger.dialogueNr;
    }
}