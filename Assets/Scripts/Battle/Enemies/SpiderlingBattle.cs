using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingBattle : EnemyBattleBase
{
    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        diceKeyNumber = 1;
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
}
