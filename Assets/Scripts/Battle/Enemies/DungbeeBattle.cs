using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungbeeBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    public Transform throwingHand;
    private int attacksFinished = 0;
    private int attacksLaunched = 0;
    private bool exposed = false;
    private bool goingBack = false;
    private bool doneRolling = false;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    [SerializeField] float rollSpeed = 1;

    void Awake()
    {
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
        startPosition = transform.position;
        targetPosition = FindObjectOfType<BattleSystem>().leftOOB.position;
        targetPosition.y = startPosition.y;
    }
    public override void EnemySetup()
    {

    }

    public void Expose()
    {
        if (isInactive)
        {
            isInactive = false;
            FindObjectOfType<BattleSystem>().SetDiceKey(FindObjectOfType<BattleSystem>().GetEnemyIndex(gameObject));
        }
    }

    public override IEnumerator EnemyAction()
    {
        if(FindObjectOfType<BattleSystem>().GetNumberOfEnemies() == 1)
        {
            attacksFinished = 0;
            attacksLaunched = 0;
            if (!exposed)
            {
                exposed = true;
                animator.SetBool("isExposed", true);
            }
            else
            {
                animator.SetBool("isAttacking", true);
            }

            while (attacksFinished < 3)
            {
                yield return null;
            }
            yield return null;
        }
        else
        {
            animator.SetBool("isAttacking", true);

            yield return StartCoroutine(GetComponent<AttackPlayerSimulation>().RollAttack(targetPosition, startPosition, rollSpeed));

            animator.SetBool("isAttacking", false);
        } 
    }

    public void DungAttack()
    {
        if(attacksLaunched < 3)
        {
            Debug.Log(attacksLaunched);
            StartCoroutine(ThrowDung());
            animator.SetBool("isAttacking", true);
        }
        if (attacksLaunched == 3)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public IEnumerator ThrowDung()
    {
        attacksLaunched++;
        var rock = Instantiate(rockToThrow, throwingHand);
        rock.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead);
        rock.StartThrow();

        while (rock != null)
        {
            yield return null;
        }
        attacksFinished++;
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
