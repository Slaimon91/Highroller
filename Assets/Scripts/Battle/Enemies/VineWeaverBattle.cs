using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineWeaverBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    [SerializeField] GameObject spiderlingPrefab;
    private int spiderlingCount = 0;
    private bool spiderlingSpawned = false;
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
        //diceKeyGO.gameObject.SetActive(false);
    }

    public void ShowDices()
    {
        //diceKeyGO.gameObject.SetActive(true);
    }

    public override IEnumerator NextDKWasActivated()
    {
        yield return StartCoroutine(EnemyAction());
    }

    public IEnumerator SpawnSpiderling()
    {
        if(spiderlingCount == 0)
        {
            spiderlingCount++;
            yield return StartCoroutine(battleSystem.SpawnNewMonster(spiderlingPrefab));
        }

        else if (spiderlingCount > 0)
        {
            spiderlingCount++;
            SpiderlingBattle firstSpiderling = FindObjectOfType<SpiderlingBattle>();
            Instantiate(spiderlingPrefab, firstSpiderling.transform.parent);
        }

        yield return null;
    }

    public override IEnumerator EnemyAction()
    {
        if (spiderlingCount < 3)
        {
            animator.SetTrigger("isAttacking");

            while(!spiderlingSpawned)
            {
                yield return null;
            }
            spiderlingSpawned = false;
            yield return null;
        }
        else
        {
            yield return null;
        }
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

    public void SetSpiderlingCount(int count)
    {
        spiderlingCount = count;
    }

    public void SpiderlingHasSpawned()
    {
        spiderlingSpawned = true;
    }
}
