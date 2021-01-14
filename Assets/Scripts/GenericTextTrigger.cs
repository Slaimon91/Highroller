using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTextTrigger : MonoBehaviour, IInteractable
{
    [TextArea(3, 20)]
    public string genericText;
    public delegate void OnFinishedText();
    public OnFinishedText onFinishedTextCallback;
    private PlayerController playerController;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void Interact()
    {
        FindObjectOfType<GenericTextManager>().DisplayText(genericText);
        playerController.onFinishedInteractingCallback += TextFinished;
    }

    private void TextFinished()
    {
        playerController.onFinishedInteractingCallback -= TextFinished;
        if (onFinishedTextCallback != null)
        {
            onFinishedTextCallback?.Invoke();
        }
    }

    public void SetText(string text)
    {
        genericText = text;
    }
}

