using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyBerry : SeedBase
{
    public override void ConsumeBerry()
    {
        if(isBerry)
        {
            base.ConsumeBerry();

            LaunchRewards rewards;
            if ((rewards = FindObjectOfType<LaunchRewards>()) != null)
            {
                rewards.LanuchHPRewardbox(10);
            }
        }
    }
}
