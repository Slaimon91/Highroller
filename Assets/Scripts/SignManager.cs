using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject signBox;

    private Queue<string> signs;
    bool readingStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        signs = new Queue<string>();
    }

    public void ReadSign(Sign signContents)
    {
        if (!readingStarted)
        {
            FindObjectOfType<PlayerController>().SetInteracting(true);
            signs.Clear();
            readingStarted = true;
            signBox.SetActive(true);

            foreach (string sign in signContents.signs)
            {
                signs.Enqueue(sign);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (signs.Count == 0)
        {
            EndDialogue();
            readingStarted = false;
            signBox.SetActive(false);
            return;
        }

        string sentence = signs.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        FindObjectOfType<PlayerController>().SetInteracting(false);
    }
}
