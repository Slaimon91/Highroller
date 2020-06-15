using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBlock : AbilityBase
{
    void Start()
    {
        FindObjectOfType<PlayerBattleController>().EnableEasyBlocking();
    }
}
