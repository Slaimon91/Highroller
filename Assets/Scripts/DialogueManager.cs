using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBox;

    private Queue<string> sentences;
    bool dialogueStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!dialogueStarted)
        {
            nameText.text = dialogue.name;

            sentences.Clear();
            dialogueStarted = true;
            dialogueBox.SetActive(true);

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            dialogueStarted = false;
            dialogueBox.SetActive(false);
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
