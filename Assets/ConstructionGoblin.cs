using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionGoblin : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            animator.SetBool("isWarning", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            animator.SetBool("isWarning", false);
        }
    }
}
