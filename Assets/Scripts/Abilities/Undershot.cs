using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undershot : AbilityBase
{
    private bool activeTurn = false;
    void Start()
    {
        FindObjectOfType<PlayerBattleController>().SetUndershot(true);
    }
    public void SetActiveTurn()
    {
        activeTurn = true; 
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