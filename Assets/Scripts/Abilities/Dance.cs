using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : AbilityBase
{
    [SerializeField] int missChance = 10;

    public override int TakeDamage()
    {
        int number = (int)Random.Range(1f, 101f);

        if(number <= missChance)
        {
            StartCoroutine(FindObjectOfType<PlayerBattleController>().MissText("Miss"));
            return 999999;
        }
        else
        {
            return base.TakeDamage();
        }
    }
}
