using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenericTextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI genericText;
    [SerializeField] GameObject textBox;

    bool readingStarted = false;

    public void DisplayText(string textContents)
    {
        if(!readingStarted)
        {
            readingStarted = true;
            textBox.SetActive(true);
            genericText.text = textContents;
            FindObjectOfType<PlayerController>().SetInteracting(true);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        readingStarted = false;
        textBox.SetActive(false);
        FindObjectOfType<PlayerController>().SetInteracting(false);
    }
}
