using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTrigger : MonoBehaviour, IInteractable
{
    public Sign signText;

    public void Interact()
    {
        FindObjectOfType<SignManager>().ReadSign(signText);
    }
}

