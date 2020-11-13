using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VineWeaverBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    
    [SerializeField] GameObject spiderlingPrefab;
    [SerializeField] GameObject spiderlingChildPrefab;
    private int spiderlingCount = 0;
    private bool spiderlingSpawned = false;
    private GameObject savedSelectedGameObject;
    private EventSystem eventSystem;
    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        battleSystem = FindObjectOfType<BattleSystem>();
        eventSystem = FindObjectOfType<EventSystem>();

        StartCoroutine(WaitBeforeDiceSetup());
    }

    IEnumerator WaitBeforeDiceSetup()
    {
        yield return new WaitForEndOfFrame();
        
        if (eventSystem.currentSelectedGameObject != null)
        {
            savedSelectedGameObject = eventSystem.currentSelectedGameObject;
            eventSystem.SetSelectedGameObject(null);
        }
        diceKeyGO.gameObject.GetComponent<Image>().enabled = false;
    }

    public void ShowDices()
    {
        diceKeyGO.gameObject.GetComponent<Image>().enabled = true;
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
        ExtraDiceSetup();
    }

    public override IEnumerator NextDKWasActivated()
    {
        yield return StartCoroutine(EnemyDamagedAction());
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
            Instantiate(spiderlingChildPrefab, firstSpiderling.transform.parent);
        }

        yield return null;
    }
    public IEnumerator EnemyDamagedAction()
    {
        if (spiderlingCount < 3)
        {
            animator.SetTrigger("isDamaged");

            while (!spiderlingSpawned)
            {
                yield return null;
            }
            spiderlingSpawned = false;
            yield return null;
        }
        else
        {
            animator.SetTrigger("isDamagedNoSpawn");
            yield return null;
        }
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
        /*if (isDead)
        {
            Destroy(gameObject);
        }*/
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
