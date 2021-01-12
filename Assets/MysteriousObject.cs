using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteriousObject : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        RandomWait();
        animator = GetComponent<Animator>();
    }

    public IEnumerator RandomWaitCoro()
    {
        float rand = Random.Range(1f, 4f);
        yield return new WaitForSeconds(rand);
        animator.SetTrigger("Fire");
    }

    public void RandomWait()
    {
        StartCoroutine(RandomWaitCoro());
    }
}
