using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenCurtain : AbilityBase
{
    [SerializeField] int damageThreshold = 5;

    void Start()
    {
        FindObjectOfType<PlayerBattleController>().SetDamageThreshold(damageThreshold);
    }
}
