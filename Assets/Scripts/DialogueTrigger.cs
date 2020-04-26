using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    public void Interact()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
