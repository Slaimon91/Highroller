using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTransition : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EnableParentAnimator()
    {
        GetComponentInParent<Dice>().EnableAnimator();
        gameObject.SetActive(false);
        animator.SetBool("normalToGold", false);
    }
}
