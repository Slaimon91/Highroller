using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiablockAnimator : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            animator.SetBool("isDisplaying", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            animator.SetBool("isDisplaying", false);
        }
    }
}
