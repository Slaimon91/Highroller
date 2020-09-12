using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileflipVisual : MonoBehaviour
{
    Animator animator;
    public delegate void FlipAnimationDone();
    public FlipAnimationDone onFlipAnimationDoneCallback;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerGaiaAnimation()
    {
        animator.SetTrigger("Gaia");
    }
    public void TriggerHPAnimation()
    {
        animator.SetTrigger("HP");
    }
    public void TriggerMonsterAnimation()
    {
        animator.SetTrigger("Monster");
    }

    public void AnimationDone()
    {
        if (onFlipAnimationDoneCallback != null)
        {
            onFlipAnimationDoneCallback.Invoke();
        }
    }
}
