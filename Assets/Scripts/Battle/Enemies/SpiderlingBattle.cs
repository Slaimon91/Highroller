using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    [SerializeField] Transform throwingHand;
    private int spiderlingNumber = 0;
    private bool doneWalking = false;
    private Vector2 firstWalkingTarget = new Vector2(0.0f+1.41f+0.15f, 1.774f-0.75f);
    private Vector2 otherWalkingTarget = new Vector2(0.75f+1.41f, 1.774f-0.75f);
    private bool attackFinished = false;
    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        spiderlingNumber = FindObjectOfType<VineWeaverBattle>().GetSpiderlingCount();
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

    public void StartWalking()
    {
        if(spiderlingNumber == 1)
        {
            StartCoroutine(Walk(firstWalkingTarget, 2f));
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
}
