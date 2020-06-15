using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoblinBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    [SerializeField] Transform throwingHand;
    private bool attackFinished = false;

    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(NewIdle());
    }

    IEnumerator NewIdle()
    {
        yield return new WaitForSeconds(Random.Range(30f, 45f));
        animator.ResetTrigger("Idle2");
        animator.ResetTrigger("Idle3");
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            animator.SetTrigger("Idle2");
        }
        else
        {
            animator.SetTrigger("Idle3");
        }
        
        StartCoroutine(NewIdle());
    }

    public override void EnemySetup()
    {

    }

    public override void Assign(bool status)
    {
        if (!status)
        {
            isAssigned = false;

            diceKeyGO.SetAssignedStatus(false, diceKeyNumber);

            animator.SetBool("isMatched", false);
        }
        else
        {
            isAssigned = true;

            diceKeyGO.SetAssignedStatus(true, diceKeyNumber);

            animator.SetBool("isMatched", true);
        }
    }

    public override IEnumerator EnemyAction()
    {
        attackFinished = false;
        animator.SetTrigger("isAttacking");

        while (!attackFinished)
        {
            yield return null;
        }
        yield return null;
    }

    public IEnumerator ThrowRock()
    {
        var rock = Instantiate(rockToThrow, throwingHand);
        rock.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead);
        rock.StartThrow();

        while (rock != null)
        {
            yield return null;
        }
        attackFinished = true;
        yield return null;
    }

    public override void TriggerDying()
    {
        animator.SetBool("isDead", true);
        isDead = true;
    }

    public override IEnumerator Die()
    {
        var orb = Instantiate(myOrb, transform);
        orb.SetTarget(FindObjectOfType<PlayerBattleController>().gaiaPoint);
        yield return StartCoroutine(orb.StartThrowCoro());
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
