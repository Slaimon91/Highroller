using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAnimator : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
