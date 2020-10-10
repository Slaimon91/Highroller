using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinespinnerBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    public override IEnumerator EnemySetup()
    {
        battleSystem.SpawnNewMonster();
        yield return null;
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
