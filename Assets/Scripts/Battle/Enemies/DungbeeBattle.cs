using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungbeeBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    public Transform throwingHand;

    void Awake()
    {
        diceKeyNumber = 8;
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }
    public override void EnemySetup()
    {

    }
    public override IEnumerator EnemyAction()
    {
        yield return null;
    }

    public override void TriggerDying()
    {
        animator.SetBool("isDead", true);
        isDead = true;
    }

    public override IEnumerator Die()
    {
        mySprite.GetComponent<ThrowSimulation>().SetTarget(FindObjectOfType<PlayerBattleController>().gaiaPoint);
        yield return StartCoroutine(mySprite.GetComponent<ThrowSimulation>().StartThrowCoro());
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
