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
    private bool distractedFinished = false;

    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }

    public override void Assign(bool status, int number)
    {
        if (!status)
        {
            isAssigned = false;

            diceKeyGO.SetAssignedStatus(false, number);

            animator.SetBool("isMatched", false);
        }
        else
        {
            isAssigned = true;

            diceKeyGO.SetAssignedStatus(true, number);

            animator.SetBool("isMatched", true);
        }
    }

    public override IEnumerator EnemyAction()
    {
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            attackFinished = false;
            animator.SetTrigger("isAttacking");

            while (!attackFinished)
            {
                yield return null;
            }
        }
        else
        {
            distractedFinished = false;
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

            while (!distractedFinished)
            {
                yield return null;
            }
        }
        
        yield return null;
    }

    public IEnumerator ThrowRock()
    {
        var rock = Instantiate(rockToThrow, throwingHand);
        rock.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead.position);
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
        RollSoulDrop();
        yield return null;
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        if (isDead)
        {
            Destroy(gameObject);
        }
    }

    public void DistractedAnimFinished()
    {
        distractedFinished = true;
    }
}
