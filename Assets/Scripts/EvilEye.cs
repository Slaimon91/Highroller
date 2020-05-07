using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EvilEye : MonoBehaviour, TTileable
{
    Animator animator;
    GroundType groundType;

    void Start()
    {
        groundType = GroundType.GreenforestWater;
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

    public GroundType GetTileType()
    {
        return groundType;
    }
}
