using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWeaverBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    [SerializeField] GameObject spiderlingPrefab;
    private int spiderlingCount = 0;
    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        battleSystem = FindObjectOfType<BattleSystem>();

        StartCoroutine(WaitBeforeDiceSetup());
    }

    IEnumerator WaitBeforeDiceSetup()
    {
        yield return new WaitForEndOfFrame();
        ExtraDiceSetup();
        diceKeyGO.gameObject.SetActive(false);
    }

    public void ShowDices()
    {
        diceKeyGO.gameObject.SetActive(true);
    }

    public void SpawnSpiderling()
    {
        if(spiderlingCount == 0)
        {
            spiderlingCount++;
            battleSystem.SpawnNewMonster(spiderlingPrefab);
        }
    }

    public override IEnumerator EnemyAction()
    {
        animator.SetBool("isAttacking", true);

        yield return null;

        animator.SetBool("isAttacking", false);
    }

    public override void TriggerDying()
    {
        animator.SetBool("isDead", true);
        isDead = true;
    }

    public override IEnumerator Die()
    {
        RollSoulDrop();
        yield return null;
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        if (isDead)
        {
            Destroy(gameObject);
        }
    }

    public int GetSpiderlingCount()
    {
        return spiderlingCount;
    }
}
