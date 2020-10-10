using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwineconeBattle : EnemyBattleBase
{
    private BattleSystem battleSystem;
    private bool goingBack = false;
    private bool doneRunning = false;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    [SerializeField] float runSpeed = 1;
    private bool done = false;
    private bool hitTarget = false;
    private bool finishedWaiting = false;

    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        battleSystem = FindObjectOfType<BattleSystem>();
        startPosition = transform.position;
        targetPosition = FindObjectOfType<BattleSystem>().playerSpawnPoint.position;
        targetPosition.x = targetPosition.x + 0.95f;
        targetPosition.y = startPosition.y;
    }

    public override IEnumerator EnemySetup()
    {
        animator.SetTrigger("isRaging");
        if (diceKeyNumber < 6)
        {
            diceKeyNumber++;
            SetDamageAmount(diceKeyNumber);
        }

        if (diceKeyGO != null)
        {
            diceKeyImage.sprite = battleSystem.diceSprites[diceKeyNumber - 1];
        }

        yield return null;
    }

    public override IEnumerator EnemyAction()
    {
        animator.SetBool("isAttacking", true);

        yield return StartCoroutine(ChargeAttack(targetPosition, startPosition, runSpeed));
    }

    public IEnumerator ChargeAttack(Vector2 target, Vector2 start, float speed)
    {
        GetComponent<ThrowSimulation>().SetTarget(startPosition);
        while (!hitTarget && !done)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, target.x))
            {
                hitTarget = true;
            }
            yield return null;
        }

        animator.SetBool("isAttacking", false);
        StartCoroutine(Wait2Sec());

        while(!finishedWaiting)
        {
            yield return null;
        }
        
        yield return GetComponent<ThrowSimulation>().StartThrowCoro();

        transform.position = startPosition;

        yield return null;
        Reset();
    }

    IEnumerator Wait2Sec()
    {
        yield return new WaitForSeconds(1f);

        finishedWaiting = true;
    }

    private void Reset()
    {
        done = false;
        hitTarget = false;
        finishedWaiting = false;
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
