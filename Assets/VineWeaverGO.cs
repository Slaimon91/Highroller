using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWeaverGO : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] GameObject monsterIconVisualGO;
    [SerializeField] Sprite background;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        StartCoroutine(StartBattle());
    }

    private IEnumerator StartBattle()
    {
        yield return StartCoroutine(FindObjectOfType<EncounterManager>().LaunchCustomBattle(monsterIconVisualGO, enemies, background));

        Destroy(gameObject);
    }
}
