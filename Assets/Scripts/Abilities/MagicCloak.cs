using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCloak : AbilityBase
{
    private PlayerBattleController player;
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
        if (GetComponentInParent<BattleAbilityHolder>().GetMarkedStatus())
        {
            activeTurn = true;
        }
    }

    public override void TurnStart()
    {
        if (activeTurn)
        {
            inactive = true;
            activeTurn = false;
            player.HealDamage(player.GetDamageTaken() / 2);

            GetComponentInParent<BattleAbilityHolder>().SetInactive();
        }
        else
        {
            base.TurnStart();
        }
    }
}
