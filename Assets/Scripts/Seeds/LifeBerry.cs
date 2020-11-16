using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBerry : SeedBase
{
    public override void ConsumeBerry()
    {
        if(isBerry)
        {
            base.ConsumeBerry();

            PlayerController player;
            if ((player = FindObjectOfType<PlayerController>()) != null)
            {
                player.LanuchHPRewardbox(666);
            }
        }
    }
}
