using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEye : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Enter", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetTrigger("Exit");
            animator.SetBool("Enter", false);
        }
    }
}
