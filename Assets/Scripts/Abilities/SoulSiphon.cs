using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSiphon : AbilityBase
{
    private PlayerBattleController player;
    [SerializeField] int healAmount = 2;

    IEnumerator Start()
    {
        while(FindObjectOfType<PlayerBattleController>() == null)
        {
            yield return null;
        }
        player = FindObjectOfType<PlayerBattleController>();
    }

    public override void EnemyDeath()
    {
        player.HealDamage(healAmount);
    }
}
