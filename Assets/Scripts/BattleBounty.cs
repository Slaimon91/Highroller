using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBounty : MonoBehaviour
{
    public int xpAmount = 0;
    public int xpMultiplier = 1;
    [SerializeField] PlayerValues playerValues;

    public static BattleBounty instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            // Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddXP(int xp)
    {
        xpAmount += xp;
    }

    public void SetXPMultiplier(int multi)
    {
        if(multi > xpMultiplier)
        {
            xpMultiplier = multi;
        }
    }

    public void GiveBounty()
    {
        FindObjectOfType<PlayerController>().LanuchBattleRewardbox(xpAmount, xpMultiplier);
        Destroy(gameObject);
    }
    
}
