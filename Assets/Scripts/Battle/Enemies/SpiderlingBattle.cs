using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingBattle : EnemyBattleBase
{
    [SerializeField] StraightProjectileSimulation slimeToThrow;
    [SerializeField] Transform throwingHand;
    private BattleSystem battleSystem;
    private int spiderlingNumber = 0;
    private bool doneWalking = false;
    private Vector2 firstWalkingTarget = new Vector2(0.0f+1.41f+0.15f, 1.774f-0.75f);
    private Vector2 otherWalkingTarget = new Vector2(0.75f+1.41f, 1.774f-0.75f);
    private Vector2 firstJumpingTarget = new Vector2(0.0f + 1.41f + 0.15f, 1.774f - 0.75f + 0.2f);
    private Vector2 secondJumpingTarget = new Vector2(0.0f + 1.41f + 0.15f, 1.774f - 0.75f + 0.4f);
    private bool attackFinished = false;
    private SpiderlingBattle spiderBrotherOne = null;
    private SpiderlingBattle spiderBrotherTwo = null;

    void Awake()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        spiderlingNumber = FindObjectOfType<VineWeaverBattle>().GetSpiderlingCount();

        if (spiderlingNumber != 1)
        {
            gameObject.transform.parent.GetChild(0).GetComponent<SpiderlingBattle>().RegisterBrother(gameObject.GetComponent<SpiderlingBattle>());
        }
    }

    public override IEnumerator EnemyAction()
    {
        if (spiderBrotherTwo != null)
        {
            yield return StartCoroutine(spiderBrotherTwo.EnemyAction());
        }
        if (spiderBrotherOne != null)
        {
            yield return StartCoroutine(spiderBrotherOne.EnemyAction());
        }
        attackFinished = false;
        animator.SetTrigger("isAttacking");

        while (!attackFinished)
        {
            yield return null;
        }

        yield return null;
    }

    public void StartWalking()
    {
        if(spiderlingNumber == 1)
        {
            StartCoroutine(Walk(firstWalkingTarget, 2f));
        }

        else
        {
            StartCoroutine(Walk(otherWalkingTarget, 2f));
        }
    }

    public IEnumerator Walk(Vector2 target, float speed)
    {
        while (!doneWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (Mathf.Approximately(transform.position.x, target.x))
            {
                doneWalking = true;
            }
            yield return null;
        }

        if (spiderlingNumber == 1)
        {
            animator.SetTrigger("finishedWalking");
        }
        else if (spiderlingNumber == 2)
        {
            animator.SetTrigger("takeoff");
            GetComponent<ThrowSimulation>().SetTarget(firstJumpingTarget);
            yield return StartCoroutine(GetComponent<ThrowSimulation>().StartThrowCoro());
            transform.position = firstJumpingTarget;
            animator.SetTrigger("hasLanded");
        }
        else if (spiderlingNumber == 3)
        {
            animator.SetTrigger("takeoff");
            GetComponent<ThrowSimulation>().SetTarget(secondJumpingTarget);
            yield return StartCoroutine(GetComponent<ThrowSimulation>().StartThrowCoro());
            transform.position = secondJumpingTarget;
            animator.SetTrigger("hasLanded");
        }

        if(FindObjectOfType<VineWeaverBattle>() != null)
        {
            FindObjectOfType<VineWeaverBattle>().SpiderlingHasSpawned();
        }

        yield return null;
    }

    public IEnumerator ThrowRock()
    {
        var slime = Instantiate(slimeToThrow, throwingHand);
        slime.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead.position);
        slime.StartShot();

        while (slime != null)
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
        if (spiderBrotherTwo != null)
        {
            spiderBrotherTwo.TriggerDying();
        }
        if (spiderBrotherOne != null)
        {
            spiderBrotherOne.TriggerDying();
        }
    }

    public override IEnumerator Die()
    {
        if(spiderlingNumber == 1)
        {
            if (FindObjectOfType<VineWeaverBattle>() != null)
                FindObjectOfType<VineWeaverBattle>().SetSpiderlingCount(0);
            FindObjectOfType<BattleSystem>().SignalEnemyDeath();
        }

        yield return null;
        
        if (isDead)
        {            
            Destroy(gameObject);
        }
    }

    public void RegisterBrother(SpiderlingBattle newBrother)
    {
        if(spiderBrotherOne == null)
        {
            spiderBrotherOne = newBrother;
        }
        else
        {
            spiderBrotherTwo = newBrother;
        }
        diceKeyNumber++;
        if (diceKeyGO != null)
        {
            diceKeyImage.sprite = battleSystem.diceSprites[diceKeyNumber - 1];
            diceKeyGO.SetDKNumber(diceKeyNumber);
        }
    }
}
