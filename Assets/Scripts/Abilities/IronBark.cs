using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBark : AbilityBase
{
    private PlayerBattleController player;
    private int stopDamage = 999999;
    private bool activeTurn = false;

    IEnumerator Start()
    {
        while (FindObjectOfType<PlayerBattleController>() == null)
        {
            yield return null;
        }
        player = FindObjectOfType<PlayerBattleController>();
    }

    public override void TurnEnd()
    {
        if(GetComponentInParent<BattleAbilityHolder>().GetMarkedStatus())
        {
            activeTurn = true;
        }
    }

    public override int TakeDamage()
    {
        if(activeTurn)
        {
            return stopDamage;
        }
        else
        {
            return base.TakeDamage();
        }
    }

    public override void TurnStart()
    {
        if (activeTurn)
        {
            inactive = true;
            activeTurn = false;

            GetComponentInParent<BattleAbilityHolder>().SetInactive();
        }
        else
        {
            base.TurnStart();
        }
    }
}
