using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savestone : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isActivated = false;
    private bool isBusy = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        if(!isBusy)
        {
            isBusy = true;

            if (isActivated)
            {
                animator.SetTrigger("isSaving");
            }
            else
            {
                isActivated = true;
                animator.SetTrigger("isActivated");
            }

            isBusy = false;
        }
    }

    private IEnumerator SavingGame()
    {
        yield return null;
    }
}
