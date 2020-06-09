using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootOfLife : AbilityBase
{
    private PlayerBattleController player;
    [SerializeField] int healAmount = 1;

    IEnumerator Start()
    {
        while (FindObjectOfType<PlayerBattleController>() == null)
        {
            yield return null;
        }
        player = FindObjectOfType<PlayerBattleController>();
    }

    public override void TurnStart()
    {
        player.HealDamage(healAmount);
    }
}
