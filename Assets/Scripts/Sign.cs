using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sign
{
    [TextArea(3, 20)]
    public string[] signs;

}


/*
 * 
 * [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] string dialogue;
    [SerializeField] bool playerInRange = false;
 * 
 * void Update()
    {

    }

    public void Interact()
    {
        if(playerInRange)
        {
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("InteractTrigger"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("InteractTrigger"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
 * 
 */
