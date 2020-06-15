using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkArmor : AbilityBase
{
    [SerializeField] int reductionAmount = 1;

    public override int TakeDamage()
    {
        return reductionAmount;
    }
}
