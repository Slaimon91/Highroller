using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TricksterBattle : EnemyBattleBase
{
    [SerializeField] GiveOWReward owReward;
    private bool attackFinished = false;
    private bool secondDiceActive = false;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 kickTargetPosition;
    [SerializeField] Transform runningStartPos;
    private BattleSystem battleSystem;
    private GameObject savedSelectedGameObject;
    [SerializeField] float runSpeed = 1;
    private EventSystem eventSystem;
    private bool hitTarget = false;
    private bool willPunch = false;
    private bool willKick = false;
    private bool runningStart = false;

    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();

        diceKeyNumber = Random.Range(1, 7);
        battleSystem = FindObjectOfType<BattleSystem>();
        eventSystem = FindObjectOfType<EventSystem>();
        startPosition = transform.position;
        targetPosition = FindObjectOfType<BattleSystem>().playerSpawnPoint.position;
        targetPosition.x = targetPosition.x + 0.91f;
        targetPosition.y = startPosition.y;
        kickTargetPosition = FindObjectOfType<PlayerBattleController>().playerBody.position;
        kickTargetPosition.x += 0.5f;
        kickTargetPosition.y = startPosition.y;
        StartCoroutine(WaitBeforeDiceSetup());
    }

    IEnumerator WaitBeforeDiceSetup()
    {
        yield return new WaitForEndOfFrame();

        ExtraDiceSetup();
    }

    public override IEnumerator NextDKWasActivated()
    {
        secondDiceActive = true;
        isGold = true;
        yield return null;
    }

    public override IEnumerator EnemySetup()
    {
        if(!secondDiceActive)
        {
            int newDiceKeyNumber = diceKeyNumber;
            while (newDiceKeyNumber == diceKeyNumber)
            {
                newDiceKeyNumber = Random.Range(1, 7);
            }
            diceKeyNumber = newDiceKeyNumber;
            battleSystem.SetDiceKey(battleSystem.GetEnemyIndex(gameObject));
            yield return null;
        }

        else
        {
            int newDiceKeyNumber = diceKeyNumber;
            while (newDiceKeyNumber == diceKeyNumber)
            {
                newDiceKeyNumber = Random.Range(2, 13);
            }
            diceKeyNumber = newDiceKeyNumber;
            battleSystem.SetDiceKey(battleSystem.GetEnemyIndex(gameObject));
            yield return null;
        }

    }

    public override IEnumerator EnemyAction()
    {
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            damageAmount = 2;
            attackFinished = false;
            hitTarget = false;
            willPunch = true;
            animator.SetTrigger("isAttacking1");

            yield return RunningToPos(targetPosition, transform.position, runSpeed);

            while (!attackFinished)
            {
                yield return null;
            }
        }
        else
        {
            damageAmount = 3;
            attackFinished = false;
            hitTarget = false;
            runningStart = true;
            animator.SetTrigger("isAttacking2");
            yield return RunningToPos(new Vector2(2, startPosition.y), transform.position, runSpeed);

            while (!attackFinished)
            {
                yield return null;
            }
        }

        yield return null;
    }

    public IEnumerator RunningToPos(Vector2 target, Vector2 start, float speed)
    {
        while (!hitTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Mathf.Approximately(transform.position.x, target.x))
            {
                hitTarget = true;
            }
            yield return null;
        }

        if(willPunch)
        {
            animator.SetTrigger("isPunching");
            willPunch = false;
        }
        else if(runningStart)
        {
            animator.SetTrigger("willKick");
            runningStart = false;
            willKick = true;
            hitTarget = false;
            StartCoroutine(RunningToPos(kickTargetPosition, transform.position, runSpeed + 2f));
        }
        else if(willKick)
        {
            yield return new WaitForSeconds(0.2f);
            animator.SetTrigger("hasKicked");
            willKick = false;
            StartCoroutine(RunningBack());
        }
    }

    public void RunBack()
    {
        StartCoroutine(RunningBack());
    }

    private IEnumerator RunningBack()
    {
        hitTarget = false;
        yield return RunningToPos(startPosition, transform.position, runSpeed);
        transform.position = startPosition;
        animator.SetTrigger("startIdle");
        attackFinished = true;
    }

    public override void TriggerDying()
    {
        animator.SetBool("isEscaping", true);
        isDead = true;
        hitTarget = false;
        StartCoroutine(Escape());
    }

    private IEnumerator Escape()
    {
        yield return RunningToPos(new Vector2(FindObjectOfType<BattleSystem>().rightOOB.position.x, startPosition.y), transform.position, runSpeed);
        StartCoroutine(Die());
    }

    public override IEnumerator Die()
    {
        yield return null;
        FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        if (owReward != null)
        {
            Instantiate(owReward);
        }
        if (isDead)
        {
            Destroy(gameObject);
        }
    }
}
