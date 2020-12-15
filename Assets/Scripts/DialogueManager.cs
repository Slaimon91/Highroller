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
    bool finishedTyping = true;
    string sentence;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if(!dialogueStarted)
        {
            FindObjectOfType<PlayerController>().SetInteracting(true);
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
        if (sentences.Count == 0 && finishedTyping)
        {
            EndDialogue();
            dialogueStarted = false;
            dialogueBox.SetActive(false);
            return;
        }

        if (finishedTyping)
        {
            sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = sentence;
            finishedTyping = true;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        finishedTyping = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        finishedTyping = true;
    }

    void EndDialogue()
    {
        FindObjectOfType<PlayerController>().SetInteracting(false);
    }
}
