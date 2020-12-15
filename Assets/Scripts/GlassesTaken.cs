using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesTaken : MonoBehaviour
{
    PlayerController playerController;
    private void Awake()
    {
        StartCoroutine(LookForPlayer());
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator LookForPlayer()
    {
        if ((playerController = FindObjectOfType<PlayerController>()) == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(LookForPlayer());
        }
        else
        {
            FindObjectOfType<MoleGlasses>().GoblinDefeated();

            Destroy(gameObject);
        }
    }
}
