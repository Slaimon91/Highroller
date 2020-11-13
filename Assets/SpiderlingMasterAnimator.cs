using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingMasterAnimator : MonoBehaviour
{
    private Animator animator;
    public float myTime;
    private AnimatorStateInfo animationState;
    private AnimatorClipInfo[] myAnimatorClip;
    void Start()
    {
        animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        animationState = animator.GetCurrentAnimatorStateInfo(0);
        myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
        //myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
        myTime = animationState.normalizedTime % 1;
        //Debug.Log(myTime);
        //Debug.Log(animationState.normalizedTime);
        //Debug.Log(animationState.normalizedTime % 1);
    }
}
