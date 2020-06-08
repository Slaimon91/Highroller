using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoblinBattle : EnemyBattleBase
{
    [SerializeField] ThrowSimulation rockToThrow;
    public Transform throwingHand;
    // Start is called before the first frame update
    void Awake()
    {
        diceKeyNumber = 3;
        if (GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(Idle2());
    }

    IEnumerator Idle2()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        animator.ResetTrigger("Idle2");
        animator.SetTrigger("Idle2");
        StartCoroutine(Idle2());
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
        //var rock = Instantiate(rockToThrow, throwingHand);
      //  rock.SetTarget(FindObjectOfType<PlayerBattleController>().playerBody);
        var rock2 = Instantiate(rockToThrow, throwingHand);
        rock2.SetTarget(FindObjectOfType<PlayerBattleController>().playerHead);
        rock2.StartThrow();

        while(rock2 != null)
        {
            yield return null;
        }
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

        if (isDead)
        {
            //Destroy(diceKeyGO.gameObject);
            Destroy(gameObject);
        }

    }
}
