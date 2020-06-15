using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedShell : AbilityBase
{
    [SerializeField] int reductionAmount = 1;

    public override int Block()
    {
        return reductionAmount;
    }
}
