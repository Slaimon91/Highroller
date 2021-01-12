using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTextTrigger : MonoBehaviour, IInteractable
{
    [TextArea(3, 20)]
    public string genericText;

    public void Interact()
    {
        FindObjectOfType<GenericTextManager>().DisplayText(genericText);
    }

    public void SetText(string text)
    {
        genericText = text;
    }
}

